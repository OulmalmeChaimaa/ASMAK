using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lecture_fichier.Models
{
    public class liste_recherche
    {
        public List<Importation_PR> listRech { get; set; }
        public List<Prelevements_Consolides>listPrelv { get; set; }
        public List<Reglements_Consolides> listReg { get; set; }
        public string Num_Declaration { get; set; }
        public string Num_Envoi_Prelevement { get; set; }
        public liste_recherche()
        {
            listRech = new List<Importation_PR>();
            listPrelv = new List<Prelevements_Consolides>();
            listReg = new List<Reglements_Consolides>();
        }
       
    }
}