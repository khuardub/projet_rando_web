using projet_rando_web.Classes;
using projet_rando_web.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Microsoft.JSInterop;

namespace projet_rando_web.Pages
{
    public partial class ModificationRandonnee
    {
        private UtilisateurSession utilisateurSession = new UtilisateurSession();
        private Randonnee randonnee;
        private List<Ville> lstVille;
        private string _selectedVilleDepartId;
        private string _selectedVilleRetourId;

        [Parameter]
        public string randoId { get; set; }

        private string SelectedVilleDepartId
        {
            get { return _selectedVilleDepartId; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _selectedVilleDepartId = value;
                    randonnee.EndroitDepart = lstVille.FirstOrDefault(v => v.Id == value);
                }
                else
                    throw new ArgumentNullException("La valeur de VilleDepart est null");
            }
        }
        private string SelectedVilleRetourId
        {
            get { return _selectedVilleRetourId; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _selectedVilleRetourId = value;
                    randonnee.EndroitRetour = lstVille.FirstOrDefault(v => v.Id == value);
                }
                else
                    throw new ArgumentNullException("La valeur de VilleRetour est null");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            randonnee = randonneeService.GetRandonnee(randoId);
/*
            if (randonnee == null)
            {
                var texte = "Cette randonnée n'existe pas.";
                //await jsRuntime.InvokeVoidAsync("alert", texte);
                //navManager.NavigateTo($"/creation_randonnee", true);
                navManager.NavigateTo();
            }*/

            lstVille = villeService.GetVilles();

            //utilisateurSession
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                utilisateurSession.Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }

        private async Task UpdateRandonnee()
        {
            if (randonnee.Auteur.Id == utilisateurSession.Id)
            {
                var result = await randonneeService.Update(randonnee, utilisateurSession.Id);
                if (result != null)
                {
                    var texte = "Modification réussie.";
                    jsRuntime.InvokeVoidAsync("localStorage.setItem", "message", texte);
                    navManager.NavigateTo($"/detail/{randonnee.Id}", true);
                }
                else
                {
                    throw new ArgumentNullException("Erreur lors de la modification de la randonnée.");
                }
            }
            else
            {
                throw new ArgumentNullException("Seul l'auteur de la randonnée peut la modifier.");
            }
        }
    }

}
