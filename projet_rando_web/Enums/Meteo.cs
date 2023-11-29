using System.ComponentModel;

namespace projet_rando_web.Enums
{
    public enum Meteo
    {
        [Description("Ensoleillé")]
        Ensoleille,
        [Description("Nuageux")]
        Nuageux,
        [Description("Enneigé")]
        Enneige,
        [Description("Pluvieux")]
        Pluvieux
        
    }
}