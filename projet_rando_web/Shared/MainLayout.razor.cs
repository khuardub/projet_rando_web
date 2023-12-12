using projet_rando_web.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace projet_rando_web.Shared
{
    public partial class MainLayout
    {
        private string userId = "";

        protected override async Task OnInitializedAsync()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            if (customAuthStateProvider != null)
            {
                userId = await customAuthStateProvider.GetUserIdFromSession();
            }
        }

        private async Task Logout()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            await customAuthStateProvider.UpdateAuthenticationState(null);
            navManager.NavigateTo("/", true);
        }
    }
}

