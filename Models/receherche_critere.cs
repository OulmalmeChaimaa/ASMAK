using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace lecture_fichier.Models
{
    public class receherche_critere
    {
        public long Id_Imp { get; set; }
        public string Code_Entite { get; set; }
        public Nullable<System.DateTime> Date_Importation { get; set; }
        public string Code_Bateau { get; set; }
        public string Banque { get; set; }
    }
}