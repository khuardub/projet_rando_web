using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel;

namespace projet_rando_web.Enums
{
    public enum Langue
    {
        Mandarin,
        Espagnol,
        Anglais,
        Hindi,
        Arabe,
        Portugais,
        Bengali,
        Russe,
        Japonais,
        [Description("Français")]
        Français,
        Allemand,
        Turc,
        Vietnamien,
        Javanais,
        Italien
    }
}
