using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace projet_rando_web.Classes
{
    public class Message
    {
        /// <summary>
        /// Obtient ou définit l'identifiant du message.
        /// </summary>
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Obtient ou définit la randonnée associée à ce message.
        /// </summary>
        [BsonIgnore]
        public Randonnee Randonnee { get; set; }

        /// <summary>
        /// Obtient ou définit la description du message.
        /// </summary>
        [BsonElement("Description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Message avec un identifiant, une randonnée et une description spécifiés.
        /// </summary>
        /// <param name="id">L'identifiant du message.</param>
        /// <param name="randonnee">La randonnée associée au message.</param>
        /// <param name="description">La description du message.</param>
        public Message(string id, Randonnee randonnee, string description)
        {
            Id = id;
            Randonnee = randonnee;
            Description = description;
        }

    }
}