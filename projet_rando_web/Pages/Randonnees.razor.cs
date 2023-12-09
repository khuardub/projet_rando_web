using projet_rando_web.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace projet_rando_web.Pages
{
    public partial class Randonnees
    {
        Randonnee _randonnee = new Randonnee();
        List<Randonnee> _randonnees;
        protected override async Task OnInitializedAsync()
        {
            _randonnees = randonneeService.GetRandonnees();
        }
        private void Reset()
        {
            _randonnee = new Randonnee();
        }
        private void Edit(string randonneeId)
        {
            _randonnee = randonneeService.GetRandonnee(randonneeId);
        }
        private void Delete(string randonneeId)
        {
            randonneeService.Delete(randonneeId);
            randonneeService.GetRandonnees();
        }
    }
}
