using System.ComponentModel;

namespace projet_rando_web.Enums
{
    public enum Meteo
    {
        [Description("Ensoleill�")]
        Ensoleille,
        Nuageux,
        [Description("Enneig�")]
        Enneige,
        Pluvieux
        
    }
}