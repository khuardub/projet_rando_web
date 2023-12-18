using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using projet_rando_web.Classes;
using projet_rando_web.Data;
using projet_rando_web.Interfaces;
using System.Security.Claims;

namespace projet_rando_web.Pages
{
    public partial class ModificationUtilisateur
    {
        private UtilisateurSession utilisateurSession = new UtilisateurSession();
        private Utilisateur utilisateur = new Utilisateur();
        bool visible = false;
        private string message = "";

        [Parameter] public string userId { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            utilisateur = await utilisateurService.GetUtilisateur(userId);
            if (utilisateur == null)
            {
                message = "Cet utilisateur n\'existe pas.";
                visible = true;
            }

            if (user.Identity.IsAuthenticated)
            {
                utilisateurSession.Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }

        private async Task UpdateUtilisateur()
        {
            Console.WriteLine("updateUtilisateur");
            if (utilisateurSession.Id == userId)
            {
                Console.WriteLine("Id ok");
                if (utilisateur != null)
                {
                    await utilisateurService.SaveOrUpdateUser(utilisateur);
                    var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
                    await customAuthStateProvider.UpdateAuthenticationState(new UtilisateurSession
                    {
                        Id = utilisateur.Id,
                        Echelon = utilisateur.Echelon,
                        Pseudo = utilisateur.Pseudo,
                        Langue = utilisateur.Langue,
                    });

                    navManager.NavigateTo($"/profil/{userId}", true);
                }
                else
                {
                    message = "Veuillez vous connecter.";
                    visible = true;
                    navManager.NavigateTo("/connexion", true);
                }
            }
            else
            {
                message = "Vous ne pouvez pas modifier le profil autre que le votre.";
                visible = true;
            }
        }

    }
}