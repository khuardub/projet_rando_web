using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projet_rando_web.Classes;
using projet_rando_web.Enums;


namespace projet_rando_web.Interfaces
{
    public interface IRandonnee
    {
        void SaveOrUpdate(Randonnee randonnee);
        string Delete(string randonneeId);
        Randonnee GetRandonnee(string randonneeId);
        List<Randonnee> GetRandonnees();

        //public string Id { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public string Nom { get; set; }
        //public string Description { get; set; }
        //public Utilisateur Utilisateur { get; set; }
        //public Pays Pays { get; set; }
        //public Ville EndroitDepart { get; set; }  
        //public DateTime DateDepart { get; set; }
        //public Ville EndroitArrivee { get; set; }
        //public DateTime DateArrivee { get; set; }
        //public Meteo Meteo { get; set; }
        //public Enums.Type Type { get; set; }
        //public int Denivele { get; set; }
        //public Statut Statut { get; set; }
    }
}