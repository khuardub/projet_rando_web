using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using projet_rando_web.Classes;
using projet_rando_web.Data;
using projet_rando_web.Enums;

namespace projet_rando_web.Pages
{
    public partial class Inscription
    {
        private Utilisateur user;

        protected override async Task OnInitializedAsync()
        {
            user = new Utilisateur();
        }

        private void CreationUtilisateur()
        {
            var userExistant = utilisateurService.GetUtilisateurByCourriel(user.Courriel);
            if (userExistant == null)
            {
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
