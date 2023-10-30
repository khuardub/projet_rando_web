using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using projet_rando_web.Classes;
using projet_rando_web.Enums;
using projet_rando_web.Data;

namespace projet_rando_web.Pages
{
    public partial class CreationRandonnee
    {
        public Randonnee randonnee;
        public VilleService _villeService;
        public List<Ville> lstVille;

        protected override async Task OnInitializedAsync()
        {
            randonnee = new Randonnee();
            randonnee.DateDepart = DateTime.UtcNow;
            lstVille = villeService.GetVilles();
        }

        public void AjoutRandonnee()
        {

        }
    }

}
