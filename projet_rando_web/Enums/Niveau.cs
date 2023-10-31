using System.ComponentModel;

namespace projet_rando_web.Enums
{
    public enum Niveau
    {
        Relax,
        Facile,
        [Description("Moderé")]
        Modere,
        Soutenu
    }
}