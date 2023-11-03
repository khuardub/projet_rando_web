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
    }
}