using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lecture_fichier.Models
{
    public class Reglements_Consolides
    {
        public int Id_Reg { get; set; }
        public long Id_Imp { get; set; }
        public string Num_OV { get; set; }
        public string Banque { get; set; }
        public string Agence { get; set; }
        public int Mnt_OV { get; set; }
        public DateTime Date_OV { get; set; }
        public string Num_Envoi_Prelevement { get; set; }


    }
}