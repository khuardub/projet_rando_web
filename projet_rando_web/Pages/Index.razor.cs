using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace projet_rando_web.Pages
{
    public partial class Index
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }
    }
}
