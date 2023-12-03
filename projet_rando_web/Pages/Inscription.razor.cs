using Microsoft.JSInterop;
using projet_rando_web.Classes;
using projet_rando_web.Data;

namespace projet_rando_web.Pages
{
    public partial class Inscription
    {
        private Utilisateur user;
        private string ConfirmMotDePasse = "";

        protected override async Task OnInitializedAsync()
        { 
            user = new Utilisateur();
        }

        private void CreationUtilisateur()
        {
            if (user.MotDePasse != ConfirmMotDePasse)
            {
                var message = $"Les mots de passe ne correspondent pas.";
                jsRuntime.InvokeVoidAsync("alert", message);
            }
            else
            {
                var userExistant = utilisateurService.GetUtilisateurByCourriel(user.Courriel);
                if (userExistant == null)
                {
                    var hashedPassword = HashPassword.HasherPassword(user.MotDePasse);
                    user.MotDePasse = hashedPassword;
                    utilisateurService.SaveOrUpdateUser(user);
                    navManager.NavigateTo("/connexion", true);
                }
                else
                {
                    var message = $"Un compte utilisateur avec ce courriel {user.Courriel} existe déjà.";
                    jsRuntime.InvokeVoidAsync("alert", message);
                }
            }

        }
    }
}
