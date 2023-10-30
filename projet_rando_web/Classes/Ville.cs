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
        [BsonElement("Nom")]
        public string Nom { get; set; }

        [BsonElement("Latitude")]
        public double Latitude { get; set; }
        [BsonElement("Longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Ville avec un identifiant, un pays et un nom spécifiés.
        /// </summary>
        /// <param name="id">L'identifiant de la ville.</param>
        /// <param name="nom">Le nom de la ville.</param>
        /// <param name="latitude">La latitude de la ville.</param>
        /// <param name="longitude">La longitude de la ville.</param>
        public Ville(string id, Pays? pays, string nom, double latitude, double longitude)
        {
            Id = id;
            Nom = nom;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}