using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel;

namespace projet_rando_web.Enums
{
    public enum Echelon
    {
        [BsonRepresentation(BsonType.String)]
        [Description("Débutant")]
        Debutant,
        [BsonRepresentation(BsonType.String)]
        [Description("Intermédiaire")]
        Intermediaire,
        [BsonRepresentation(BsonType.String)]
        [Description("Expert")]
        Expert
    }
}
