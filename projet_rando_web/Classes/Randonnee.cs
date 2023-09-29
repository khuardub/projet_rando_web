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
    public class Randonnee : IRandonnee
    {

        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CreatedAt"), BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("Nom"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "Le nom est requis.")]
        [MaxLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères.")]
        [MinLength(5, ErrorMessage = "Le nom doit avoir au moins 5 caractères")]
        public string Nom { get; set; }

        [BsonElement("Description"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "La description est requis.")]
        [MaxLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères.")]
        [MinLength(10, ErrorMessage = "Le nom doit avoir au moins 10 caractères")]
        public string Description { get; set; }

        [BsonIgnore]
        public Utilisateur Utilisateur { get; set; }

        [BsonIgnore]
        public Pays Pays { get; set; }

        [BsonIgnore]
        public Ville VilleDepart { get; set; }

        [BsonElement("DateDepart"), BsonRepresentation(BsonType.DateTime)]
        public DateTime DateDepart { get; set; }

        [BsonIgnore]
        public Ville VilleArrivee { get; set; }

        [BsonElement("DateDepart"), BsonRepresentation(BsonType.DateTime)]
        public DateTime DateArrivee { get; set; }

        [BsonIgnore]
        public Meteo Meteo { get; set; }

        [BsonElement("Type"), BsonRepresentation(BsonType.String)]
        public Enums.Type Type { get; set; }

        [BsonElement("Denivele"), BsonRepresentation(BsonType.String)]
        public string Denivele { get; set; }

        [BsonElement("Statut"), BsonRepresentation(BsonType.String)]
        public Statut Statut { get; set; }

        public Randonnee()
        {
        }
        public Randonnee(string id, DateTime createdAt, string nom, string description, Utilisateur utilisateur, Pays pays, Ville villeDepart, DateTime dateDepart, Ville villeArrivee, DateTime dateArrivee, Meteo meteo, string denivele, Statut statut)
        {
            this.Id = id;
            this.CreatedAt = createdAt;
            this.Nom = nom;
            this.Description = description;
            this.Utilisateur = utilisateur;
            this.Pays = pays;
            this.VilleDepart = villeDepart;
            this.DateDepart = dateDepart;
            this.VilleArrivee = villeArrivee;
            this.DateArrivee = dateArrivee;
            this.Meteo = meteo;
            this.Denivele = denivele;
            this.Statut = statut;

        }

    }
}