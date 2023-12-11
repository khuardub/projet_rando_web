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
        Task<string> Update(Randonnee randonnee, string auteurId);
        string Delete(string randonneeId);
        Randonnee GetRandonnee(string randonneeId);
        List<Randonnee> GetRandonnees();
        Task<string> Insert(Randonnee randonnee, string utilisateurId);

        bool RandonneeExiste(Randonnee randonnee);

        List<Utilisateur> GetParticipants(Randonnee randonnee);

        Task<string> InsertParticipant(Randonnee randonnee, string utilisateurId);

        Task<string> RemoveParticipant(Randonnee randonnee, string utilisateurId);
    }
}