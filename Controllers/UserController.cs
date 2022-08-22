using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using lecture_fichier.Models;

namespace lecture_fichier.Controllers
{
    public class UserController : Controller
    {
        // GET: User


        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    
                    var message = "";
                    string dataFile = "";
                    if (System.IO.Path.GetExtension(filename) == ".txt") {
                        dataFile = Path.Combine(Server.MapPath("~/FileUpload"), filename);
                        file.SaveAs(dataFile);
                        if (System.IO.File.Exists(dataFile))
                        {
                            // string d = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                            // filename = filename.Split('.')[0] +"(" + System.IO.Path.(filename) + ")" + System.IO.Path.GetExtension(filename);
                            //filename = filename.Split('.')[0] + (1) + System.IO.Path.GetExtension(filename);
                            //filename = "chaimaa oulmalme" + System.IO.Path.GetExtension(filename);

                            // dataFile = Path.Combine(Server.MapPath("~/FileUpload"), filename);
                            
                        }


                        var result = "";

                        Array userData = null;
                        char[] delimiter1 = { '$' };
                        char[] delimiter2 = { '#' };
                        char[] delimiter3 = { ';' };

                        //var dataFile = Server.MapPath("~/FileUpload/Prvmois.txt");

                        if (System.IO.File.Exists(dataFile))
                        {
                            userData = System.IO.File.ReadAllLines(dataFile);
                            if (userData == null)
                            {
                                // Empty file.
                                result = "The file is empty.";
                                ViewBag.Message2 = result;
                            }
                        }
                        /*else
                        {
                            // File does not exist.
                            result = "The file does not exist.";
                            ViewBag.Message2 = result;

                        }*/

                        if (result == "")
                        {
                            long id = 3;
                            foreach (string dataline in userData)
                            {
                                var cpt = 1;
                                foreach (string dataIm in dataline.Split(delimiter1))
                                {
                                    if (!dataIm.Contains("#"))
                                    {
                                        if (dataIm.Contains(";"))
                                        {
                                            string[] res = dataIm.Split(delimiter3);
                                            ConsoCNSSEntities model;
                                            model = new ConsoCNSSEntities();
                                            Importation_PR imp = new Importation_PR();
                                            var h = res[6];
                                            var fichier = model.Importation_PR.Where(x => x.Num_Envoi == h).FirstOrDefault();
                                            if (fichier == null)
                                            {
                                                if (res.Length > 1)
                                                {
                                                    message = "";

                                                    imp.Code_Entite = res[0];
                                                    string date = res[1] + "/" + res[2];
                                                    imp.Date_Importation = Convert.ToDateTime(date);
                                                    imp.Mois = res[1];
                                                    imp.Annee = res[2];
                                                    imp.Type_Peche = res[3];
                                                    imp.Code_Correspondance = res[4];
                                                    imp.Desig_Correspondance = res[5];
                                                    imp.Num_Envoi = res[6];
                                                    model.Importation_PR.Add(imp);
                                                    model.SaveChanges();
                                                    id = imp.Id_Imp;
                                                }
                                            }
                                            else
                                            {
                                                message = "le fichier est déja importé";
                                                ViewBag.Message1 = message;
                                            }
                                        }
                                    }
                                    else if (message == "")
                                    {
                                        cpt = cpt + 1;
                                        foreach (string data in dataIm.Split(delimiter2))
                                        {
                                            if (cpt == 2)
                                            {
                                                string[] res2 = data.Split(delimiter3);
                                                ConsoCNSSEntities model;
                                                model = new ConsoCNSSEntities();
                                                Prelevements_Consolides pre = new Prelevements_Consolides();
                                                if (res2.Length > 2)
                                                {

                                                    /* var s = res2[4];
                                                     var fich = model.Prelevements_Consolides.Where(x => x.Num_Declaration == s).FirstOrDefault();
                                                     if (fich == null)
                                                     {*/
                                                    pre.Id_Imp = id;
                                                    pre.Date_Exploitation = Convert.ToDateTime(res2[0]);
                                                    pre.Code_Bateau = res2[1];
                                                    pre.Nom_Bateau = res2[2];
                                                    pre.Type_Bateau = res2[3];
                                                    pre.Num_Declaration = res2[4];
                                                    pre.Mnt_Vente = double.Parse(res2[5], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                                    pre.Mnt_Brut_Operation = double.Parse(res2[6], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                                    pre.Mnt_COM = float.Parse(res2[7], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                                    pre.Mnt_TVA = float.Parse(res2[8], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                                    pre.Mnt_Net_Operation = float.Parse(res2[9], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                                    model.Prelevements_Consolides.Add(pre);
                                                    model.SaveChanges();

                                                }
                                            }
                                            else if (cpt == 3)
                                            {
                                                string[] res3 = data.Split(delimiter3);
                                                ConsoCNSSEntities model;
                                                model = new ConsoCNSSEntities();
                                                Reglements_Consolides reg = new Reglements_Consolides();
                                                if (res3.Length > 2)
                                                {
                                                    var c = res3[5];
                                                    var fi = model.Reglements_Consolides.Where(x => x.Num_Envoi_Prelevement == c).FirstOrDefault();

                                                    if (fi == null)
                                                    {
                                                        reg.Id_Imp = id;
                                                        reg.Num_OV = res3[0];
                                                        reg.Banque = res3[1];
                                                        reg.Agence = res3[2];
                                                        reg.Mnt_OV = double.Parse(res3[3], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                                        reg.Date_OV = Convert.ToDateTime(res3[4]);
                                                        reg.Num_Envoi_Prelevement = res3[5];
                                                        model.Reglements_Consolides.Add(reg);
                                                        model.SaveChanges();

                                                    }
                                                }
                                            }
                                        }
                                        ViewBag.Message = "Upload file successfully";

                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        ViewBag.text = "veillez inserer un fichier de type text";

                    }


                }

                return View();
            }
            catch
            {
                ViewBag.Message1 = "file not saved";
                return View();
            }
        }

        public ActionResult recherche()
        {

            ConsoCNSSEntities model;
            model = new ConsoCNSSEntities();
            liste_recherche listRecherche;
            liste_recherche list = new liste_recherche();
            ViewBag.alert = "";
            list.Num_Declaration = "";
            list.Num_Envoi_Prelevement = "";

            var l = (from o in model.Importation_PR
                     select new
                     { o }).ToList();
            foreach (var i in l)
            {
                list.listRech.Add(i.o);
            }

            return View("recherche",list);
        }

        [HttpPost] 
        public ActionResult recherche1(string Num_Envoi, string Num_Declaration, string Num_Envoi_Prelevement)
        {
            ConsoCNSSEntities model;
            model = new ConsoCNSSEntities();
            liste_recherche listRecherche;
            listRecherche = new liste_recherche();

            
            listRecherche.Num_Declaration = Num_Declaration;
            listRecherche.Num_Envoi_Prelevement = Num_Envoi_Prelevement;
            
            ViewBag.test2 = listRecherche.Num_Declaration;
            ViewBag.test3 = listRecherche.Num_Envoi_Prelevement;
            if (Num_Envoi !="")
            {
                if(Num_Declaration !="" && Num_Envoi_Prelevement != "")
                {
                    // model.Database.ExecuteSqlCommand("select * from Importation_PR I, Prelevements_Consolides P, Reglements_Consolides R where I.Num_Envoi = Num_Envoi and I.Id_Imp = P.Id_Imp and I.Id_Imp = R.Id_Imp and (P.Num_Declaration = Num_Declaration and R.Num_Envoi_Prelevement = Num_Envoi_Prelevement)");
                    var list=(from I in model.Importation_PR
                    join P in model.Prelevements_Consolides on I.Id_Imp equals P.Id_Imp
                    join R in model.Reglements_Consolides on I.Id_Imp equals R.Id_Imp
                    where I.Num_Envoi == Num_Envoi
                    && P.Num_Declaration == Num_Declaration
                    && R.Num_Envoi_Prelevement == Num_Envoi_Prelevement
                    select new
                    {
                        
                        Id_Imp = I.Id_Imp,
                        Code_Entite = I.Code_Entite,
                        Date_Importation = I.Date_Importation,
                        Mois=I.Mois,
                        Annee=I.Annee,
                        Type_Peche=I.Type_Peche,
                        Code_Correspondance=I.Code_Correspondance,
                        Desig_Correspondance=I.Desig_Correspondance,
                        Num_Envoi=I.Num_Envoi
                        // other assignments
                    }).ToList();

                    foreach (var item in list)
                    {
                        Importation_PR r = new Importation_PR();
                        r.Id_Imp = item.Id_Imp;
                        r.Code_Entite = item.Code_Entite;
                        r.Date_Importation = item.Date_Importation;
                        r.Mois = item.Mois;
                        r.Annee = item.Annee;
                        r.Type_Peche = item.Type_Peche;
                        r.Code_Correspondance = item.Code_Correspondance;
                        r.Desig_Correspondance = item.Desig_Correspondance;
                        r.Num_Envoi = item.Num_Envoi;
                        if (r!=null)
                        listRecherche.listRech.Add(r);
                        ViewBag.alert = "now";
                    }
                    
                }
                else if (Num_Declaration == "" && Num_Envoi_Prelevement == "")
                {
                    //model.Database.ExecuteSqlCommand("select * from Importation_PR I, Prelevements_Consolides P, Reglements_Consolides R where I.Num_Envoi = Num_Envoi and I.Id_Imp = P.Id_Imp and I.Id_Imp = R.Id_Imp ");
                    var list = (from I in model.Importation_PR
                                join P in model.Prelevements_Consolides on I.Id_Imp equals P.Id_Imp
                                join R in model.Reglements_Consolides on I.Id_Imp equals R.Id_Imp
                                where I.Num_Envoi == Num_Envoi
                                select new
                                {

                                    Id_Imp = I.Id_Imp,
                                    Code_Entite = I.Code_Entite,
                                    Date_Importation = I.Date_Importation,
                                    Mois = I.Mois,
                                    Annee = I.Annee,
                                    Type_Peche = I.Type_Peche,
                                    Code_Correspondance = I.Code_Correspondance,
                                    Desig_Correspondance = I.Desig_Correspondance,
                                    Num_Envoi = I.Num_Envoi
                                    // other assignments
                                }).Distinct().ToList();

                    foreach (var item in list)
                    {
                        Importation_PR r = new Importation_PR();
                        r.Id_Imp = item.Id_Imp;
                        r.Code_Entite = item.Code_Entite;
                        r.Date_Importation = item.Date_Importation;
                        r.Mois = item.Mois;
                        r.Annee = item.Annee;
                        r.Type_Peche = item.Type_Peche;
                        r.Code_Correspondance = item.Code_Correspondance;
                        r.Desig_Correspondance = item.Desig_Correspondance;
                        r.Num_Envoi = item.Num_Envoi;
                        if (r != null)
                            listRecherche.listRech.Add(r);
                        ViewBag.alert = "now";
                    }
                    listRecherche.Num_Declaration = Num_Declaration;
                    listRecherche.Num_Envoi_Prelevement = Num_Envoi_Prelevement;
                }
                else
                {
                    //model.Database.ExecuteSqlCommand("select * from Importation_PR I, Prelevements_Consolides P, Reglements_Consolides R where I.Num_Envoi = Num_Envoi and I.Id_Imp = P.Id_Imp and I.Id_Imp = R.Id_Imp and (P.Num_Declaration = Num_Declaration or R.Num_Envoi_Prelevement = Num_Envoi_Prelevement)");
                    var list = (from I in model.Importation_PR
                                join P in model.Prelevements_Consolides on I.Id_Imp equals P.Id_Imp
                                join R in model.Reglements_Consolides on I.Id_Imp equals R.Id_Imp
                                where I.Num_Envoi == Num_Envoi
                                && (P.Num_Declaration == Num_Declaration
                                || R.Num_Envoi_Prelevement == Num_Envoi_Prelevement)
                                select new
                                {

                                    Id_Imp = I.Id_Imp,
                                    Code_Entite = I.Code_Entite,
                                    Date_Importation = I.Date_Importation,
                                    Mois = I.Mois,
                                    Annee = I.Annee,
                                    Type_Peche = I.Type_Peche,
                                    Code_Correspondance = I.Code_Correspondance,
                                    Desig_Correspondance = I.Desig_Correspondance,
                                    Num_Envoi = I.Num_Envoi
                                    // other assignments
                                }).Distinct().ToList();

                    foreach (var item in list)
                    {
                        Importation_PR r = new Importation_PR();
                        r.Id_Imp = item.Id_Imp;
                        r.Code_Entite = item.Code_Entite;
                        r.Date_Importation = item.Date_Importation;
                        r.Mois = item.Mois;
                        r.Annee = item.Annee;
                        r.Type_Peche = item.Type_Peche;
                        r.Code_Correspondance = item.Code_Correspondance;
                        r.Desig_Correspondance = item.Desig_Correspondance;
                        r.Num_Envoi = item.Num_Envoi;
                        if (r != null)
                            listRecherche.listRech.Add(r);
                        ViewBag.alert = "now";
                    }
                }
            }
            else if (Num_Declaration != "" && Num_Envoi_Prelevement != "")
            {
                //model.Database.ExecuteSqlCommand("select * from Importation_PR I, Prelevements_Consolides P, Reglements_Consolides R where I.Id_Imp = P.Id_Imp and I.Id_Imp = R.Id_Imp and (P.Num_Declaration = Num_Declaration and R.Num_Envoi_Prelevement = Num_Envoi_Prelevement)");
                var list = (from I in model.Importation_PR
                            join P in model.Prelevements_Consolides on I.Id_Imp equals P.Id_Imp
                            join R in model.Reglements_Consolides on I.Id_Imp equals R.Id_Imp
                            where P.Num_Declaration == Num_Declaration
                            && R.Num_Envoi_Prelevement == Num_Envoi_Prelevement
                            select new
                            {

                                Id_Imp = I.Id_Imp,
                                Code_Entite = I.Code_Entite,
                                Date_Importation = I.Date_Importation,
                                Mois = I.Mois,
                                Annee = I.Annee,
                                Type_Peche = I.Type_Peche,
                                Code_Correspondance = I.Code_Correspondance,
                                Desig_Correspondance = I.Desig_Correspondance,
                                Num_Envoi = I.Num_Envoi
                                // other assignments
                            }).Distinct().ToList();

                foreach (var item in list)
                {
                    Importation_PR r = new Importation_PR();
                    r.Id_Imp = item.Id_Imp;
                    r.Code_Entite = item.Code_Entite;
                    r.Date_Importation = item.Date_Importation;
                    r.Mois = item.Mois;
                    r.Annee = item.Annee;
                    r.Type_Peche = item.Type_Peche;
                    r.Code_Correspondance = item.Code_Correspondance;
                    r.Desig_Correspondance = item.Desig_Correspondance;
                    r.Num_Envoi = item.Num_Envoi;
                    if (r != null)
                        listRecherche.listRech.Add(r);
                    ViewBag.alert = "now";
                }
            }
            else if (Num_Declaration == "" && Num_Envoi_Prelevement == "")
            {
                ViewBag.Vide = "veillez remplir un champs";
            }
            else { //model.Database.ExecuteSqlCommand("select * from Importation_PR I, Prelevements_Consolides P, Reglements_Consolides R where I.Id_Imp = P.Id_Imp and I.Id_Imp = R.Id_Imp and (P.Num_Declaration = Num_Declaration or R.Num_Envoi_Prelevement = Num_Envoi_Prelevement)");
                var list = (from I in model.Importation_PR
                            join P in model.Prelevements_Consolides on I.Id_Imp equals P.Id_Imp
                            join R in model.Reglements_Consolides on I.Id_Imp equals R.Id_Imp
                            where (P.Num_Declaration == Num_Declaration
                            || R.Num_Envoi_Prelevement == Num_Envoi_Prelevement)
                            select new
                            {

                                Id_Imp = I.Id_Imp,
                                Code_Entite = I.Code_Entite,
                                Date_Importation = I.Date_Importation,
                                Mois = I.Mois,
                                Annee = I.Annee,
                                Type_Peche = I.Type_Peche,
                                Code_Correspondance = I.Code_Correspondance,
                                Desig_Correspondance = I.Desig_Correspondance,
                                Num_Envoi = I.Num_Envoi
                                // other assignments
                            }).Distinct().ToList();

                foreach (var item in list)
                {
                    Importation_PR r = new Importation_PR();
                    r.Id_Imp = item.Id_Imp;
                    r.Code_Entite = item.Code_Entite;
                    r.Date_Importation = item.Date_Importation;
                    r.Mois = item.Mois;
                    r.Annee = item.Annee;
                    r.Type_Peche = item.Type_Peche;
                    r.Code_Correspondance = item.Code_Correspondance;
                    r.Desig_Correspondance = item.Desig_Correspondance;
                    r.Num_Envoi = item.Num_Envoi;
                    if (r != null)
                        listRecherche.listRech.Add(r);
                    ViewBag.alert = "now";
                }
            }
            return View("recherche", listRecherche) ;
        }

        public ActionResult GetPrelevement(string Num_Declaration, long Id_Imp)
        {
            liste_recherche list = new liste_recherche();
            ConsoCNSSEntities model;
            model = new ConsoCNSSEntities();
            ViewBag.test = Num_Declaration;
            ViewBag.test2 = Id_Imp;
            //prod.listProd = model.PRODUITs.Where(o => o.Nom == nom || o.Prix <= prixmax || o.Prix >= prixmin).Select(o => new produit() { ProduitID = o.ProduitID, Nom = o.Nom, Prix = o.Prix, Photo = o.Photo }).ToList();
           // var l =model.Prelevements_Consolides.Where(o => o.Num_Declaration == Num_Declaration && o.Id_Imp == Id_Imp).Select(o => new Prelevements_Consolides() { Date_Exploitation = o.Date_Exploitation, Code_Bateau = o.Code_Bateau, Nom_Bateau = o.Nom_Bateau, Type_Bateau = o.Type_Bateau, Num_Declaration = o.Num_Declaration, Mnt_Vente = o.Mnt_Vente, Mnt_Brut_Operation = o.Mnt_Brut_Operation, Mnt_TVA = o.Mnt_TVA, Mnt_COM = o.Mnt_COM, Mnt_Net_Operation = o.Mnt_Net_Operation }).ToList();
            // list.listPrelv = model.Prelevements_Consolides.Where(o => o.Num_Declaration == Num_Declaration && o.Id_Imp == Id_Imp).Select new ( o=> new Prelevements_Consolides() ).ToList();
            if(Num_Declaration != "")
            {
                var l = (from o in model.Prelevements_Consolides
                                              where o.Num_Declaration == Num_Declaration
                                              && o.Id_Imp==Id_Imp
                                              select new
                                              { o }).ToList();
                foreach (var i in l)
                {
                    list.listPrelv.Add(i.o);
                }

            }
            else
            {
                var l = (from o in model.Prelevements_Consolides
                         where o.Id_Imp == Id_Imp
                         select new
                         { o }).ToList();
                foreach (var i in l)
                {
                    list.listPrelv.Add(i.o);
                }

            }

          


            return View("GetPrelevement",list);
        }

        public ActionResult GetReglement(string Num_Envoi_Prelevement, long Id_Imp)
        {
            liste_recherche list = new liste_recherche();
            ConsoCNSSEntities model;
            model = new ConsoCNSSEntities();
            if (Num_Envoi_Prelevement != "")
            {
                var l = (from o in model.Reglements_Consolides
                         where o.Num_Envoi_Prelevement == Num_Envoi_Prelevement
                         && o.Id_Imp == Id_Imp
                         select new
                         { o }).ToList();
                foreach (var i in l)
                {
                    list.listReg.Add(i.o);
                }

            }
            else
            {
                var l = (from o in model.Reglements_Consolides
                         where o.Id_Imp == Id_Imp
                         select new
                         { o }).ToList();
                foreach (var i in l)
                {
                    list.listReg.Add(i.o);
                }

            }




            return View("GetReglement", list);
        }

        public ActionResult delete()
        {
            ConsoCNSSEntities model;
            model = new ConsoCNSSEntities();
            model.Database.ExecuteSqlCommand(" TRUNCATE TABLE [Prelevements_Consolides]");
            model.Database.ExecuteSqlCommand(" TRUNCATE TABLE [Reglements_Consolides]");
           // model.Database.ExecuteSqlCommand(" TRUNCATE TABLE [Importation_PR]");
            ViewBag.Message2 = "suppression avec succés";
            return View("remplir");
        }
        
        
            
        
    }
}