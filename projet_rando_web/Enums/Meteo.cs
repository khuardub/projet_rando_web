using System.ComponentModel;

namespace projet_rando_web.Enums
{
    public enum Meteo
    {
        [Description("Ensoleillé")]
        Ensoleille,
        Nuageux,
        [Description("Enneigé")]
        Enneige,
        Pluvieux
        
    }
}