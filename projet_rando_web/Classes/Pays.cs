using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace projet_rando_web.Classes
{
    public class Pays
    {
        /// <summary>
        /// Obtient ou définit l'identifiant du pays.
        /// </summary>
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Obtient ou définit le nom du pays.
        /// </summary>
        [BsonElement("Nom"), BsonRepresentation(BsonType.String)]
        public string Nom { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Pays avec un identifiant et un nom spécifiés.
        /// </summary>
        /// <param name="id">L'identifiant du pays.</param>
        /// <param name="nom">Le nom du pays.</param>
        public Pays(string id, string nom)
        {
            Id = id;
            Nom = nom;
        }
    }
}