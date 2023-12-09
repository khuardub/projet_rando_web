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
                var message = await randonneeService.InsertParticipant(randonnee, utilisateurId);
                if (message == "Inscription réussie")
                {
                    var texte = "Félicitations! Préparez - vous pour une aventure mémorable.";
                    await jsRuntime.InvokeVoidAsync("alert", texte);
                    // renvoie page perso
                    //navManager.NavigateTo("/profil", true);
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
                    await jsRuntime.InvokeVoidAsync("alert", texte);
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
    }

}