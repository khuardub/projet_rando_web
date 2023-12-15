using System.Runtime.CompilerServices;
using projet_rando_web.Classes;
using Microsoft.AspNetCore.Components;
using dymaptic.GeoBlazor.Core.Components.Layers;
using dymaptic.GeoBlazor.Core.Components.Views;
using dymaptic.GeoBlazor.Core.Components.Geometries;
using dymaptic.GeoBlazor.Core.Components.Symbols;
using dymaptic.GeoBlazor.Core.Objects;
using projet_rando_web.Data;
using System.Security.Claims;
using Microsoft.JSInterop;
using projet_rando_web.Enums;

namespace projet_rando_web.Pages
{
    public partial class DetailRandonnee
    {
        private UtilisateurSession utilisateurSession = new UtilisateurSession();
        Randonnee randonnee;
        string meteo;
        string type;
        string status;
        string niveau;
        bool visible = false;
        private string message = "";

        [Parameter]
        public string Id { get; set; }
        public int grosseurChunk = 100;
        public GraphicsLayer graphicsLayer;
        private MapView? mapView;


        protected override async Task OnInitializedAsync()
        {
            randonnee = randonneeService.GetRandonnee(Id);
            meteo = randonnee.Meteo.GetDescription();
            type = randonnee.TypeRando.GetDescription();
            status = randonnee.Statut.GetDescription();
            niveau = randonnee.Niveau.GetDescription();

            //utilisateurSession
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            
            if (user.Identity.IsAuthenticated)
            {
                utilisateurSession.Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var texte = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "message");

                if (!string.IsNullOrEmpty(texte))
                {
                    message = texte;
                    visible = true;
                    await jsRuntime.InvokeVoidAsync("localStorage.removeItem", "message");
                }
            }
        }

        private async Task OnMapRendered()
        {
            graphicsLayer = new GraphicsLayer();
            Graphic graph;
            Geometry point = GeneratePoint(randonnee);
            graph = new Graphic(point, GenerateSimpleMarker());
            graphicsLayer.Add(graph);
            mapView.AddLayer(graphicsLayer);
        }
        private Symbol GenerateSimpleMarker()
        {
            return new SimpleMarkerSymbol(new Outline(new MapColor(33, 25, 51)),
                new MapColor(67, 131, 168), 10);
        }
        private Point GeneratePoint(Randonnee rando)
        {
            return new Point(rando.EndroitDepart.Longitude, rando.EndroitDepart.Latitude);
        }


        private async Task InscriptionParticipant()
        {
            var utilisateurId = utilisateurSession.Id;
            if (utilisateurId != null)
            {
                var result = await randonneeService.InsertParticipant(randonnee, utilisateurId);
                if (result == "Inscription réussie")
                {
                    var texte = "Félicitations! Préparez - vous pour une aventure mémorable.";
                    message = texte;
                    visible = true;
                }
                else
                {
                    await jsRuntime.InvokeVoidAsync("alert", message);
                }
            }
        }

        private bool EstParticipant()
        {
            var utilisateurId = utilisateurSession.Id;
            if (utilisateurId != null)
            {
                return randonnee.Participants.Any(u => u.Id == utilisateurId);
            }
            return false;
        }

        private async Task DesinscriptionParticipant()
        {
            var utilisateurId = utilisateurSession.Id;
            if(utilisateurId != null)
            {
                var result = await randonneeService.RemoveParticipant(randonnee, utilisateurId);
                if (result == "Désinscription réussie.")
                {
                    var texte = "Vous êtes désinscrit à la randonnée.";
                    //jsRuntime.InvokeVoidAsync("localStorage.setItem", "message", texte);
                    message = texte;
                    visible = true;
                }
                else
                {
                    await jsRuntime.InvokeVoidAsync("alert", result);
                }
            }
            else
            {
                var texte = "Veuillez vous connecter à votre compte.";
                await jsRuntime.InvokeVoidAsync("alert", texte);
            }
        }

        private async Task ModifierRandonnee()
        {
            navManager.NavigateTo($"/modification/{randonnee.Id}", true);
        }

        private async Task ArchiverRandonnee()
        {
            var isArchive = randonnee.IsArchive;
            var response = await randonneeService.ArchiverRandonnee(randonnee, !isArchive);
            if (response == "Modification de l\'archivage effectué.")
            {
                randonnee.IsArchive = !isArchive;
                var texte = "Modification de l'archivage effectuée.";
                message = texte;
                visible = true;
                // forcer le rafraichissement de la vue pour changer le bouton
                StateHasChanged();
            }
            else
            {
                var texte = "Modification non effectuée. Veuillez contacter un administrateur.";
                message = texte;
                visible = true;
            }
        }
    }

}