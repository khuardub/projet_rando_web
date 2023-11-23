using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace projet_rando_web.Enums
{
    public enum Echelon
    {
        [BsonRepresentation(BsonType.String)]
        Debutant,
        [BsonRepresentation(BsonType.String)]
        Intermediaire,
        [BsonRepresentation(BsonType.String)]
        Confirme
    }
}
