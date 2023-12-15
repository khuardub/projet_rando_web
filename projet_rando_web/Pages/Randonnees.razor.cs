using projet_rando_web.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace projet_rando_web.Pages
{
    public partial class Randonnees
    {
        List<Randonnee> _randonnees = new List<Randonnee>();
        protected override async Task OnInitializedAsync()
        {
            _randonnees = await randonneeService.GetRandonneesAVenirNonArchive();
        }
        
    }
}
