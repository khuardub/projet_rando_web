using projet_rando_web.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace projet_rando_web.Pages
{
    public partial class Randonnees
    {
        Randonnee _randonnee = new Randonnee();
        List<Randonnee> _randonnees; 
        // protected override async Task OnInitializedAsync()
        //protected override async Task OnInitializedAsync()
        //{
        //    return GetRandonnees();
        //}
        private List<Randonnee> GetRandonnees()
        {
            return _randonnees = new List<Randonnee>{

                new Randonnee("1", "Randonnée pittoresque",
                    "Une randonnée pittoresque à travers les montagnes, offrant des vues à couper le souffle."),
                new Randonnee("2", "Randonnée pleine nature",
                    "Une aventure en pleine nature à travers une forêt dense avec des sentiers sinueux."),
                new Randonnee("3", "Randonnée relaxante",
                    "Une randonnée relaxante le long d'une rivière paisible, idéale pour une journée en famille."),
                new Randonnee("4", "Randonnée expédition",
                    "Une expédition exigeante jusqu'au sommet d'une montagne escarpée, réservée aux randonneurs expérimentés."),
                new Randonnee("5", "Randonnée exploration",
                    "Une exploration urbaine à travers la ville, découvrant l'histoire et la culture locales."),
                new Randonnee("6", "Randonnée aventure",
                    "Une aventure en bord de mer, longeant la côte avec des plages de sable blanc et des vues magnifiques sur l'océan."),
            };
        }
        private void Save()
        {
            randonneeService.SaveOrUpdate(_randonnee);
            Reset();
            GetRandonnees();
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
            GetRandonnees();
        }
    }
}
