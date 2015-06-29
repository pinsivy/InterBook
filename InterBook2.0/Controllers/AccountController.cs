using InterBook2._0.BLL;
using InterBook2._0.BLL.Mail;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace InterBook2._0.Controllers
{
    public class AccountController : BaseController
    {
        // INSCRIPTION
        // GET: Account/SignUp
        [HttpGet]
        public ActionResult SignUp()
        {
            SignUpModel sum = new SignUpModel();

            //PREREMPLISSAGE
            if (SessionManager.Current.Util != null && SessionManager.Current.Util.Util_Email != null)
                sum.Email = SessionManager.Current.Util.Util_Email.email;

            //REMPLIR LISTE EXPERIENCE
            List<Ref_ExperienceSimple> re = UtilManager.GetExperiences();
            foreach (Ref_ExperienceSimple reTemp in re)
            {
                sum.ExperienceList.Add(new SelectListItem { Text = reTemp.Description, Value = reTemp.id_Experience.ToString() });
            }

            //REMPLIR LISTE REGION/DEPARTEMENT
            List<Ref_Departement> lrd = UtilManager.GetCompletDepartements();
            sum.DepartementList = lrd.ToList().Select(t => new GroupedSelectListItem
            {
                GroupKey = t.Ref_Region.id_Region.ToString(),
                GroupName = t.Ref_Region.Description,
                Text = t.Description,
                Value = t.id_Departement.ToString()
            });

            return View(sum);
        }

        // INSCRIPTION
        // POST: Account/SignUp
        [HttpPost]
        public ActionResult SignUp(SignUpModel model, HttpPostedFileBase inputFileJpg, HttpPostedFileBase inputFilePdf)
        {
            //ALLER SUR /Dashboard
            //if (model.IsValid())
            if(true)
            {

                int numFileJpg = save(inputFileJpg, "inputFileJpg", new string[] { ".jpg", ".gif", ".png" }, "photo");
                int numFilePdf = save(inputFilePdf, "inputFilePdf", new string[] { ".pdf" }, "cv");

                ////INSERTION BDD UTIL_EMAIL
                if (SessionManager.Current.Util.Util_Email.email != model.Email)
                {
                    //SessionManager.Current.Util.Util_Email = UtilEmailManager.ReturnUtilEmailByEmail(model.Email);
                }

                ////INSERTION BDD UTIL_POSTAL
                Util_Postal up = new Util_Postal
                {
                    dCrea = DateTime.Now,
                    id_Civilite = model.Civilite,
                    nom = Capitalize(model.Nom),
                    prenom = Capitalize(model.Prenom)
                };
                Util_PostalSimple ups = new Util_PostalSimple
                {
                    dCrea = DateTime.Now,
                    id_Civilite = model.Civilite,
                    nom = Capitalize(model.Nom),
                    prenom = Capitalize(model.Prenom)
                };
                
                if(UtilPostalManager.GetRefVilleByDescription(model.Ville) != null)
                    up.id_Ville = UtilPostalManager.GetRefVilleByDescription(model.Ville).id_Ville;
                
                //INSERTION BDD UTIL
                UtilPostalManager.InsertLine(ups);

                //////INSERTION BDD UTIL_INFO
                Util_Info ui = new Util_Info
                {
                    photo = numFileJpg,
                    cv = numFilePdf,
                    permisA = model.PermisA,
                    permisB = model.PermisB,
                    permisC = model.PermisC,
                    numTel = model.NumTel,
                    dNaissance = model.DateDeNaissance,
                    experience = int.Parse(model.Experience.ToString()),
                    motivation = model.Motivation
                };
                Util_InfoSimple uis = new Util_InfoSimple
                {
                    photo = numFileJpg,
                    cv = numFilePdf,
                    permisA = model.PermisA,
                    permisB = model.PermisB,
                    permisC = model.PermisC,
                    numTel = model.NumTel,
                    dNaissance = model.DateDeNaissance,
                    experience = int.Parse(model.Experience.ToString()),
                    motivation = model.Motivation
                };
                UtilInfoManager.InsertLine(uis);

                foreach (string p in model.lProfession)
                {
                    Ref_ProfessionSimple rf = UtilProfessionManager.GetRefProfessionByDescription(p.ToString());

                    if (rf != null)
                    {
                        UtilProfessionManager.InsertLine(new Util_ProfessionSimple
                        {
                            id_Profession = rf.id_Profession,
                            IdU = SessionManager.Current.Util.IdU
                        });
                    }
                }

                foreach (string idD in model.SubmittedDepartement)
                {
                    UtilGeoManager.InsertLine(new Util_GeoSimple
                    {
                        id_Departement = idD,
                        idU = SessionManager.Current.Util.IdU
                    });
                }

                SessionManager.Current.Util.Util_Postal = up;
                SessionManager.Current.Util.Util_Info = ui;

                foreach(string d in model.lDate.Split(';'))
                {
                    if(d != "")
                        UtilDispoManager.ajoutDispo(new DateTime(int.Parse(d.Split('/')[2]), int.Parse(d.Split('/')[1]), int.Parse(d.Split('/')[0])), SessionManager.Current.Util.IdU);
                }

                return this.SetPageRedirect("DashBoard", "Disponibility");
            }

            return View(model);
        }

        private int save(HttpPostedFileBase file, string inputFile, string[] AllowedFileExtensions, string dossier)
        {
            if (file == null)
            {
                ModelState.AddModelError(inputFile, "Please Upload Your file");
            }
            else if (file.ContentLength > 0)
            {
                int MaxContentLength = 1024 * 1024 * 3; //3 MB

                if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower()))
                {
                    ModelState.AddModelError(inputFile, "Please file of type : " + string.Join(", ", AllowedFileExtensions));
                }

                else if (file.ContentLength > MaxContentLength)
                {
                    ModelState.AddModelError(inputFile, "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                }
                else
                {
                    //TO:DO
                    int counter = 1;
                    string path = "";
                    if (AllowedFileExtensions.Contains(".pdf"))
                    {
                        path = Path.Combine(Server.MapPath("~/Media/file/upload/"), dossier + counter.ToString() + ".pdf");
                        while (System.IO.File.Exists(path))
                        {
                            counter++;
                            path = Path.Combine(Server.MapPath("~/Media/file/upload/"), dossier + counter.ToString() + ".pdf");
                        }
                    }
                    else
                    {
                        path = Path.Combine(Server.MapPath("~/Media/file/upload/"), dossier + counter.ToString() + ".jpg");
                        while (System.IO.File.Exists(path))
                        {
                            counter++;
                            path = Path.Combine(Server.MapPath("~/Media/file/upload/"), dossier + counter.ToString() + ".jpg");
                        }
                    }
                    file.SaveAs(path);
                    return counter;
                }
            }
            return 0;
        }

        // INSCRIPTION
        // POST: Account/SignUpHome
        [HttpPost]
        public JsonResult SignUpHome(string particulier, string email, string mdp)
        {
            Util_EmailSimple utilEmail = UtilEmailManager.GetUtilEmailByEmail(email, (particulier == "1"));
            if (utilEmail != null)
            {
                return Json(new { Success = true, Message = "connu" });
            }
            else
            {
                ////INSERTION BDD UTIL_EMAIL
                Util_EmailSimple ue = UtilEmailManager.ReturnUtilEmailByEmail(email);

                //Hachage MdP
                //byte[] data = System.Text.Encoding.ASCII.GetBytes(mdp);
                //data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                //String mdpHash = System.Text.Encoding.Default.GetString(data);

                var secret = "interbook";
                var encoding = new System.Text.ASCIIEncoding();
                byte[] keyByte = encoding.GetBytes(secret);
                byte[] messageBytes = encoding.GetBytes(mdp);
                byte[] hashmessage = new System.Security.Cryptography.HMACSHA256(keyByte).ComputeHash(messageBytes);
                String mdpHash = Convert.ToBase64String(hashmessage);

                //INSERTION BDD UTIL
                UtilSimple u = new UtilSimple
                {
                    dCrea = DateTime.Now,
                    id_Declinaison_Culture = 1,
                    idu_Email = ue.idu_Email,
                    mdp = mdpHash,
                    uid = DefaultValueManager.ReturnSQLGuid(),
                    particulier = (particulier == "1")
                };
                UtilManager.InsertLine(u, true);
                //SessionManager.Current.Util.Util_Email = ue;

                //Insertion Consentement
                UtilConsentementManager.InsertConsentement(null, u.IdU, 1, 0);
                UtilConsentementManager.InsertConsentement(null, u.IdU, 2, 1);

                //Envoi de l'email de confirmaion
                MailBase mb = new MailBase("Confirmez votre compte", "Voir ce mail sur ordinateur ?", "InterBook", BaseManager.EmailNoReply, 1, SessionManager.Current.Util, SessionManager.Current.Util.Util_Email);
                Dictionary<String, String> varsAdd = new Dictionary<String, String>{};
                MailManager.SendMail(mb, varsAdd);

                return Json(new { Success = true, Message = "200" });
            }
        }

        public static string Capitalize(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length == 1)
                return value.ToUpper();

            return char.ToUpper(value[0]) + value.Substring(1).ToLower();

        }

        //
        // GET: Account/Criterions
        [HttpGet]
        public ActionResult SignUpE()
        {
            return View(new SignUpEModel());
        }

        //
        // POST: Account/Criterions
        [HttpPost]
        public ActionResult SignUpE(SignUpEModel model, HttpPostedFileBase inputFileJpg)
        {
            //ALLER SUR /Dashboard
            //if (model.IsValid())
            if (true)
            {

                int numFileJpg = save(inputFileJpg, "inputFileJpg", new string[] { ".jpg", ".gif", ".png" }, "logo");

                ////INSERTION BDD UTIL_EMAIL
                if (SessionManager.Current.Util.Util_Email.email != model.Email)
                {
                    //SessionManager.Current.Util.Util_Email = UtilEmailManager.ReturnUtilEmailByEmail(model.Email);
                }

                ////INSERTION BDD UTIL_POSTAL
                Util_Postal up = new Util_Postal
                {
                    dCrea = DateTime.Now,
                    id_Civilite = model.Civilite,
                    nom = Capitalize(model.Nom),
                    prenom = Capitalize(model.Prenom)
                };
                Util_PostalSimple ups = new Util_PostalSimple
                {
                    dCrea = DateTime.Now,
                    id_Civilite = model.Civilite,
                    nom = Capitalize(model.Nom),
                    prenom = Capitalize(model.Prenom)
                };

                //INSERTION BDD UTIL
                UtilPostalManager.InsertLine(ups);

                //////INSERTION BDD UTIL_INFO
                Util_Info ui = new Util_Info
                {
                    photo = numFileJpg,
                    numTel = model.NumTel,
                    dNaissance = model.DateDeNaissance
                };
                Util_InfoSimple uis = new Util_InfoSimple
                {
                    photo = numFileJpg,
                    numTel = model.NumTel,
                    dNaissance = model.DateDeNaissance
                };
                UtilInfoManager.InsertLine(uis);

                Util_Info_Entreprise uie = new Util_Info_Entreprise
                {
                    Nom = model.NomEntreprise,
                    Email = model.EmailEntreprise,
                    Ville = model.VilleEntreprise,
                    Tel = model.NumTelEntreprise,
                    Fax = model.Fax,
                    APE = model.APE,
                    Logo = numFileJpg,
                    Siret = model.Siret,
                    Siren = model.Siren,
                    SiteWeb = model.SiteWeb,
                };
                Util_Info_EntrepriseSimple uies = new Util_Info_EntrepriseSimple
                {
                    Nom = model.NomEntreprise,
                    Email = model.EmailEntreprise,
                    Ville = model.VilleEntreprise,
                    Tel = model.NumTelEntreprise,
                    Fax = model.Fax,
                    APE = model.APE,
                    Logo = numFileJpg,
                    Siret = model.Siret,
                    Siren = model.Siren,
                    SiteWeb = model.SiteWeb,
                };
                UtilInfoManager.InsertLine(uies);

                
                SessionManager.Current.Util.Util_Postal = up;
                SessionManager.Current.Util.Util_Info = ui;
                SessionManager.Current.Util.Util_Info_Entreprise = uie;

                return this.SetPageRedirect("DashBoard", "Disponibility");
            }

            return View(model);
        }

        // CONNEXION
        // GET: Account/SignIn
        [HttpGet]
        public ActionResult SignIn()
        {
            SignInModel cm = new SignInModel();
            return View(cm);
        }

        // CONNEXION
        // POST: Account/SignIn
        [HttpPost]
        public JsonResult SignIn(string particulier, string email, string mdp)
        {
            //Hachage MdP
            //byte[] data = System.Text.Encoding.ASCII.GetBytes(mdp);
            //data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            //String mdpHash = System.Text.Encoding.Default.GetString(data);

            var secret = "interbook";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(mdp);
            byte[] hashmessage = new System.Security.Cryptography.HMACSHA256(keyByte).ComputeHash(messageBytes);
            String mdpHash = Convert.ToBase64String(hashmessage);

            bool f = particulier == "1";

            UtilSimple util = UtilManager.GetUtilByEmailMdp(f, email, mdpHash);
            if (util == null)
            {
                return Json(new { Success = true, Reponse = "0", Message = "inconnu" });
            }
            SessionManager.BuildSession(email);

            if (util != null)
                return Json(new { Success = true, Reponse = "1", Message = "connu" });
            else
                return Json(new { Success = true, Reponse = "2", Message = email });
        }

        private byte[] GetFichier(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] fichier = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return fichier;
        }


        // GET: Account/GetRegions
        [HttpGet]
        public JsonResult GetRegions()
        {
            List<Ref_RegionSimple> rps = UtilManager.GetRegions();
            if (rps == null)
            {
                return Json(null);
            }

            return Json(rps, JsonRequestBehavior.AllowGet);
        }

        // GET: Account/GetDepartements
        [HttpGet]
        public JsonResult GetDepartements()
        {
            List<Ref_DepartementSimple> rps = UtilManager.GetDepartements();
            if (rps == null)
            {
                return Json(null);
            }

            return Json(rps, JsonRequestBehavior.AllowGet);
        }

        // POST: Account/ChangeCulture
        [HttpPost]
        public JsonResult ChangeCulture(string c)
        {
            SessionManager.Current.CurrentUICulture = new CultureInfo(c);

            return Json(new { Success = true, Reponse = "200", Message = "" });
        }
    }
}
