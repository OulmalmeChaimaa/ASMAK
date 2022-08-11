using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lecture_fichier.Models
{
    public class Importation_PR
    {
        public long Id_Imp { get; set; }
        public string Code_Entite { get; set; }
        public DateTime Date_Importation { get; set; }
        public string Mois { get; set; }
        public string Annee { get; set; }
        public string Type_Peche { get; set; }
        public string Code_Correspondance { get; set; }
        public string Desig_Correspondance { get; set; }
        public string Num_Envoi { get; set; }


    }
}