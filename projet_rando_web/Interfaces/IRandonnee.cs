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
        Task SaveOrUpdate(Randonnee randonnee);
        string Delete(string randonneeId);
        Randonnee GetRandonnee(string randonneeId);
        List<Randonnee> GetRandonnees();
        // Doublon avec le code SaveOrUpdate
        void Insert(Randonnee randonnee);

        bool RandonneeExiste(Randonnee randonnee);

        Task<string> InsertParticipant(Randonnee randonnee, string utilisateurId);
    }
}