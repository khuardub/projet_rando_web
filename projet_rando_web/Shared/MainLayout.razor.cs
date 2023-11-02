using projet_rando_web.Data;

namespace projet_rando_web.Shared
{
    public partial class MainLayout
    {
        private async Task Logout()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            await customAuthStateProvider.UpdateAuthenticationState(null);
            navManager.NavigateTo("/", true);
        }
    }
}

