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
        /// <summary>
        /// Obtient ou définit l'identifiant de l'utilisateur.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Obtient ou définit le mot de passe de l'utilisateur.
        /// </summary>
        public string MotDePasse { get; set; }

        /// <summary>
        /// Obtient ou définit le pseudo de l'utilisateur.
        /// </summary>
        public string Pseudo { get; set; }

        /// <summary>
        /// Obtient ou définit le prénom de l'utilisateur.
        /// </summary>
        public string Prenom { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de l'utilisateur.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Obtient ou définit l'adresse e-mail de l'utilisateur.
        /// </summary>
        public string Courriel { get; set; }

        /// <summary>
        /// Obtient ou définit la langue de l'utilisateur.
        /// </summary>
        public Langue Langue { get; set; }

        /// <summary>
        /// Obtient ou définit le niveau d'accès de l'utilisateur.
        /// </summary>
        public Niveau Niveau { get; set; }

    }
}