using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace projet_rando_web.Classes
{
    public class Langue
    {
        /// <summary>
        /// Représente une langue.
        /// </summary>
        /// <value>Identifiant de la langue.</value>
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de la langue.
        /// </summary>
        /// <value>Le nom de la langue.</value>
        [BsonElement("Nom"), BsonRepresentation(BsonType.String)]
        public string Nom { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Langue avec un nom spécifié.
        /// </summary>
        /// <param name="nom">Le nom de la langue.</param>
        public Langue(string nom)
        {
            Nom = nom;
        }

    }
}