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
        public RandonneeService randonneeService;
        public List<string> lstVilleTempo = new List<string>()
        {
            "Quebec",
            "Moteal",
            "Liban"
        };

        protected override void OnInitialized()
        {
            randonnee = new Randonnee();
            randonnee.DateDepart = DateTime.UtcNow;
            //randonneeService = new RandonneeService();
        }

        public void AjoutRandonnee()
        {

        }
    }
    
}
