using Microsoft.JSInterop;
using projet_rando_web.Classes;
using projet_rando_web.Data;
using System.ComponentModel.DataAnnotations;

namespace projet_rando_web.Pages
{
    public partial class Connexion
    {
        private List<ValidationResult> validationResults = new List<ValidationResult>();
        private class User
        {

            [Required(ErrorMessage = "L'adresse courriel est requise.")]
            [EmailAddress(ErrorMessage = "L'adresse courriel n'est pas valide.")]
            public string Courriel { get; set; }

            [Required(ErrorMessage = "Le mot de passe est requis.")]
            public string MotDePasse { get; set; }
        }

        private User user = new User();

        private async Task SeConnecter()
        {
            var validationContext = new ValidationContext(user, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(user, validationContext, validationResults, validateAllProperties: true))
            {
                foreach (var validationResult in validationResults)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }

                return;
            }

            var utilisateur = utilisateurService.GetUtilisateurByCourriel(user.Courriel);
            if (utilisateur == null || utilisateur.MotDePasse != user.MotDePasse)
            {
                validationResults.Add(new ValidationResult("Le courriel ou le mot de passe est incorrect.", new[] { nameof(user.Courriel), nameof(user.MotDePasse) }));

                return;
            }

            var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            await customAuthStateProvider.UpdateAuthenticationState(new UtilisateurSession
            {
                Id = utilisateur.Id,
                Echelon = utilisateur.Echelon,
                Pseudo = utilisateur.Pseudo,
                Langue = utilisateur.Langue,
            });
            navManager.NavigateTo("/", true);
        }

    }
}
