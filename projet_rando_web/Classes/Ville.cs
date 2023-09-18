using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace projet_rando_web.Classes
{
    public class Ville
    {
        /// <summary>
        /// Obtient ou définit l'identifiant de la ville.
        /// </summary>
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Obtient ou définit le pays auquel appartient la ville.
        /// </summary>
        [BsonIgnore]
        public Pays Pays { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de la ville.
        /// </summary>
        [BsonElement("Nom"), BsonRepresentation(BsonType.String)]
        public string Nom { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Ville avec un identifiant, un pays et un nom spécifiés.
        /// </summary>
        /// <param name="id">L'identifiant de la ville.</param>
        /// <param name="pays">Le pays auquel appartient la ville.</param>
        /// <param name="nom">Le nom de la ville.</param>
        public Ville(string id, Pays pays, string nom)
        {
            Id = id;
            Pays = pays;
            Nom = nom;
        }
    }
}