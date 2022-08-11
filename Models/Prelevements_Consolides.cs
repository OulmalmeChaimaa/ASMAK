using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lecture_fichier.Models
{
    public class Prelevements_Consolides
    {
        public int Id_PC { get; set; }
        public long Id_Imp { get; set; }
        public DateTime Date_Exploitation { get; set; }
        public string Code_Bateau { get; set; }
        public string Nom_Bateau { get; set; }
        public string Type_Bateau { get; set; }
        public string Num_Declaration { get; set; }
        public double Mnt_Vent { get; set; }
        public double Mnt_Brut_Operation { get; set; }
        public double Mnt_COM { get; set; }
        public double Mnt_TVA { get; set; }
        public double Mnt_Net_Operation { get; set; }



    }
}