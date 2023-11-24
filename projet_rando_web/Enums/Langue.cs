using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel;

namespace projet_rando_web.Enums
{
    public enum Langue
    {
        [Description("Mandarin")]
        Mandarin,
        [Description("Espagnol")]
        Espagnol,
        [Description("Anglais")]
        Anglais,
        [Description("Hindi")]
        Hindi,
        [Description("Arabe")]
        Arabe,
        [Description("Portugais")]
        Portugais,
        [Description("Bengalil")]
        Bengali,
        [Description("Russe")]
        Russe,
        [Description("Japonais")]
        Japonais,
        [Description("Français")]
        Français,
        [Description("Allemand")]
        Allemand,
        [Description("Turc")]
        Turc,
        [Description("Vietnamien")]
        Vietnamien,
        [Description("Javanais")]
        Javanais,
        [Description("Italien")]
        Italien
    }
}
