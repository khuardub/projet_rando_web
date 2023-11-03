using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projet_rando_web.Enums;
using projet_rando_web.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace projet_rando_web.Classes
{
    /// <summary>
    /// Représente un utilisateur.
    /// </summary>
    [Serializable]
    public class Utilisateur
    {
        /// <summary>
        /// Obtient ou définit l'identifiant de l'utilisateur.
        /// </summary>
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Obtient ou définit le mot de passe de l'utilisateur.
        /// </summary>
        [BsonElement("MotDePasse"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [MaxLength(25, ErrorMessage = "Le mot de passe ne peut pas dépasser 25 caractères.")]
        [MinLength(5, ErrorMessage = "Le mot de passe doit avoir au moins 5 caractères.")]
        public string MotDePasse { get; set; }

        /// <summary>
        /// Obtient ou définit le pseudo de l'utilisateur.
        /// </summary>
        [BsonElement("Pseudo"), BsonRepresentation(BsonType.String)]
        public string Pseudo { get; set; }

        /// <summary>
        /// Obtient ou définit le prénom de l'utilisateur.
        /// </summary>
        [BsonElement("Prenom"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "Le prénom est requis.")]
        [MaxLength(50, ErrorMessage = "Le prénom ne peut pas dépasser 50 caractères.")]
        [MinLength(5, ErrorMessage = "Le prénom doit avoir au moins 5 caractères.")]
        public string Prenom { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de l'utilisateur.
        /// </summary>
        [BsonElement("Nom"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "Le nom est requis.")]
        [MaxLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères.")]
        [MinLength(5, ErrorMessage = "Le nom doit avoir au moins 5 caractères.")]
        public string Nom { get; set; }

        /// <summary>
        /// Obtient ou définit l'adresse courriel de l'utilisateur.
        /// </summary>
        [BsonElement("Courriel"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "L'adresse courriel est requise.")]
        [EmailAddress(ErrorMessage = "L'adresse courriel n'est pas valide.")]
        public string Courriel { get; set; }

        /// <summary>
        /// Obtient ou définit la langue de l'utilisateur.
        /// </summary>
        [BsonElement("Langue")]
        public Langue Langue { get; set; }

        /// <summary>
        /// Obtient ou définit le niveau d'accès de l'utilisateur.
        /// </summary>
        [BsonElement("Echelon")]
        public Echelon Echelon { get; set; }

        /// <summary>
        /// Constructeur vide
        /// </summary>
        public Utilisateur()
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Utilisateur avec un identifiant spécifié.
        /// </summary>
        public Utilisateur(string id, string motDePasse, string pseudo, string prenom, string nom, string courriel, Langue langue, Echelon echelon)
        {
            this.Id = id;
            this.MotDePasse = motDePasse;
            this.Pseudo = pseudo;
            this.Prenom = prenom;
            this.Nom = nom;
            this.Courriel = courriel;
            this.Langue = langue;
            this.Echelon = echelon;

        }
    }
}
