using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using projet_rando_web.Classes;
using projet_rando_web.Enums;
using projet_rando_web.Data;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace projet_rando_web.Pages
{
    public partial class CreationRandonnee
    {

        #region CONSTANTES


        #endregion

        #region ATTRIBUTS

        private Randonnee randonnee;
        private List<Ville> lstVille;
        private string _selectedVilleDepartId;
        private string _selectedVilleRetourId;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS
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
        #endregion


        #region MÉTHODES
        private void AjoutRandonnee()
        {
            if (RandonneeValid(randonnee))
            {
                if (!randonneService.RandonneeExiste(randonnee))
                {
                    randonnee.CreatedAt = DateTime.UtcNow;
                    randonneService.Insert(randonnee);
                    navigation.NavigateTo($"/detail/{randonnee.Id}", true);
                }
                else
                {
                    randonneExiste = true;
                }

            }
            else
                throw new ArgumentNullException("La randonnée n'est pas valid");
        }

        protected override async Task OnInitializedAsync()
        {
            randonnee = new Randonnee();
            randonnee.DateDepart = DateTime.UtcNow;
            lstVille = villeService.GetVilles();
        }

        private static bool RandonneeValid(Randonnee randonnee)
        {
            var validationContext = new ValidationContext(randonnee);
            var validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(randonnee, validationContext, validationResults, true);
        }

        #endregion
    }

}
