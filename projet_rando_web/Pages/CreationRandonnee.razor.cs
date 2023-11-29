using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using projet_rando_web.Classes;
using projet_rando_web.Enums;
using projet_rando_web.Data;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace projet_rando_web.Pages
{
    public partial class CreationRandonnee
    {
        private Randonnee randonnee;
        private List<Ville> lstVille;
        private string _selectedVilleDepartId;
        private string _selectedVilleRetourId;
        private UtilisateurSession utilisateurSession = new UtilisateurSession();

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

        [Inject]
        private NavigationManager navigation { get; set; }

        private bool randonneExiste = false;

        private async Task AjoutRandonnee()
        {
            if (RandonneeValid(randonnee))
            {
                if (!randonneService.RandonneeExiste(randonnee))
                {
                    var utilisateurId = utilisateurSession.Id;
                    if (utilisateurId != null)
                    {
                        randonnee.CreatedAt = DateTime.UtcNow;
                        await randonneService.Insert(randonnee, utilisateurId);
                        navigation.NavigateTo($"/detail/{randonnee.Id}", true);
                    }
                    else
                    {
                        throw new ArgumentNullException("L'utilisateur de la session n'existe pas.");
                    }
                }
                else
                {
                    randonneExiste = true;
                }

            }
            else
                throw new ArgumentNullException("La randonnée n'est pas valide");
        }

        protected override async Task OnInitializedAsync()
        {
            randonnee = new Randonnee();
            randonnee.DateDepart = DateTime.UtcNow;
            lstVille = villeService.GetVilles();

            //utilisateurSession
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                utilisateurSession.Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }

        private static bool RandonneeValid(Randonnee randonnee)
        {
            var validationContext = new ValidationContext(randonnee);
            var validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(randonnee, validationContext, validationResults, true);
        }

    }

}
