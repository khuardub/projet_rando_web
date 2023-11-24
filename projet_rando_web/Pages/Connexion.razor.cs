using Microsoft.JSInterop;
using projet_rando_web.Classes;
using projet_rando_web.Data;

namespace projet_rando_web.Pages
{
    public partial class Connexion
    {
        private class User
        {
            public string Courriel { get; set; }

            public string MotDePasse { get; set; }
        }
        private User user = new User();

        private async Task SeConnecter()
        {
            var utilisateur = utilisateurService.GetUtilisateurByCourriel(user.Courriel);
            if (utilisateur == null || utilisateur.MotDePasse != user.MotDePasse)
            {
                await jsRuntime.InvokeVoidAsync("alert", "Désolé. L'ensemble courriel et mot de passe est invalide.");
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
