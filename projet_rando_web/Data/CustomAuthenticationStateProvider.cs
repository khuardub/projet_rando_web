using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using projet_rando_web.Classes;

namespace projet_rando_web.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //UtilisateurSession resultat 
                var userSessionStorage = await _sessionStorage.GetAsync<UtilisateurSession>("UtilisateurSession");
                // return boolean
                var utilisateurSession = userSessionStorage.Success ? userSessionStorage.Value : null;
                if (utilisateurSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, utilisateurSession.Id),
                    new Claim(ClaimTypes.Name, utilisateurSession.Pseudo),
                    new Claim(ClaimTypes.Role, utilisateurSession.Echelon.ToString()),
                    new Claim(ClaimTypes.Locality, utilisateurSession.Langue.ToString()),
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        // Login ou Logout utilisateur
        public async Task UpdateAuthenticationState(UtilisateurSession utilisateurSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (utilisateurSession != null)
            {
                await _sessionStorage.SetAsync("UtilisateurSession", utilisateurSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, utilisateurSession.Pseudo),
                    new Claim(ClaimTypes.Role, utilisateurSession.Echelon.ToString()),
                    new Claim(ClaimTypes.Locality, utilisateurSession.Langue.ToString()),
                }));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UtilisateurSession");
                claimsPrincipal = _anonymous;
            }

            // Changement de status
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
