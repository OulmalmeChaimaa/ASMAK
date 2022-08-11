using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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
                    bool exist = false ;
                    var message = "";
                    string dataFile="";
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
                            exist = true;
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