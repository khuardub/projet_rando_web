using System.Reflection.Metadata;
using projet_rando_web.Classes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using projet_rando_web.Data;


namespace projet_rando_web.Pages
{
    public partial class DetailRandonnee
    {
        Randonnee randonnee;
        private string randonneeId;

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            randonnee = randonneeService.GetRandonnee(Id);
        }
    }
}