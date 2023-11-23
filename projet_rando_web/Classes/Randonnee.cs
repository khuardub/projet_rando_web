using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projet_rando_web.Enums;
using projet_rando_web.Classes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using Type = System.Type;

namespace projet_rando_web.Classes
{
    public class Randonnee
    {

        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        [BsonElement("CreatedAt"), BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("Auteur")]
        public Utilisateur Auteur { get; set; }

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

        [BsonElement("Participants")]
        public List<Utilisateur>? Participants { get; set; }

        //[BsonIgnore]
        //public Pays Pays { get; set; }
        
        [BsonElement("EndroitDepart")]
        [Required(ErrorMessage ="L'endroit de départ est requis.")]
        public Ville EndroitDepart { get; set; }

        [BsonElement("EndroitRetour")]
        [Required(ErrorMessage = "L'endroit de retour est requis.")]
        public Ville? EndroitRetour { get; set; }



        [BsonElement("DateDepart"), BsonRepresentation(BsonType.DateTime)]
        public DateTime DateDepart { get; set; }

        [BsonElement("Meteo")]
        [Required(ErrorMessage ="Météo est requis.")]
        public Meteo Meteo { get; set; }

        [BsonElement("Sorte")]
        [Required(ErrorMessage = "Le type est requis.")]
        public Sorte TypeRando { get; set; }

        [BsonElement("Denivele"), BsonRepresentation(BsonType.Int32)]
        public int Denivele { get; set; }

        [BsonElement("Statut")]
        public Statut Statut { get; set; }

        [BsonElement("Niveau")]
        [Required(ErrorMessage = "Le niveau est requis.")]
        public Niveau Niveau { get; set; }

        public Randonnee()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        
        public Randonnee(string id, Utilisateur auteur, string nom, string description, Ville villeDepart, Ville villeRetour, Meteo meteo, DateTime dateDepart, Sorte typeRando, int denivele, Statut statut, Niveau niveau)
        {
            this.Id = id;
            this.Auteur = auteur;
            this.Nom = nom;
            this.Description = description;
            this.EndroitDepart = villeDepart;
            this.EndroitRetour = villeRetour;
            this.Meteo = meteo;
            this.DateDepart = dateDepart;
            this.TypeRando = typeRando;
            this.Denivele = denivele;
            this.Statut = statut;
            this.Niveau = niveau;
            this.Participants = new List<Utilisateur>();
        }
    }
}