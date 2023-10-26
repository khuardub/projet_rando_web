using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projet_rando_web.Classes;
using projet_rando_web.Enums;

namespace projet_rando_web.Interfaces
{
    public interface IUtilisateur
    {
        void SaveOrUpdateUser(Utilisateur utilisateur);

        Utilisateur GetUtilisateur(string utilisateurId);
        string DeleteUser(string utilisateurId);
        List<Utilisateur> GetUtilisateurs();

    }
}