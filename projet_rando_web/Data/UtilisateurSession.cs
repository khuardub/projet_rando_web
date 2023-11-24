﻿using projet_rando_web.Enums;

namespace projet_rando_web.Data
{
    public class UtilisateurSession
    {
        public string Id { get; set; }
        public string Pseudo { get; set; }
        public Echelon Echelon { get; set; }
        public Langue Langue { get; set; }
    }
}
