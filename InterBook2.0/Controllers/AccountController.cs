using InterBook2._0.BLL;
using InterBook2._0.BLL.Mail;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

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
            if (SessionManager.Current.Util != null && SessionManager.Current.Util.Util_Email != null)
                sum.Email = SessionManager.Current.Util.Util_Email.email;
            return View(sum);
        }

        // INSCRIPTION
        // POST: Account/SignUp
        [HttpPost]
        public ActionResult SignUp(SignUpModel model)
        {
            //ALLER SUR /Dashboard
            if (ModelState.IsValid)
            {
                ////INSERTION BDD UTIL_EMAIL
                if(SessionManager.Current.Util.Util_Email.email != model.Email)
                {
                    SessionManager.Current.Util.Util_Email = UtilEmailManager.ReturnUtilEmailByEmail(model.Email);
                    UtilEmailManager.InsertLine(SessionManager.Current.Util.Util_Email);
                }

                ////INSERTION BDD UTIL_POSTAL
                Util_Postal up = new Util_Postal
                {
                    dCrea = DateTime.Now,
                    id_Civilite = model.Civilite,
                    nom = Capitalize(model.Nom),
                    prenom = Capitalize(model.Prenom),
                    permis = model.Permis,
                    numeroTel = model.NumTel.ToString()
                };
                UtilPostalManager.InsertLine(up);

                //INSERTION BDD UTIL
                Util u = SessionManager.Current.Util;
                u.idu_Postal = up.idu_Postal;

                UtilManager.InsertLine(u, true);
                SessionManager.Current.Util.Util_Postal = up;

                return this.SetPageRedirect("Account", "Criterions");
            }

            return View(model);
        }

        // INSCRIPTION
        // POST: Account/SignUpHome
        [HttpPost]
        public JsonResult SignUpHome(string email, string mdp)
        {
            Util_Email utilEmail = UtilEmailManager.GetUtilEmailByEmail(email);
            if (utilEmail != null)
            {
                return Json(new { Success = true, Message = "connu" });
            }
            else
            {
                ////INSERTION BDD UTIL_EMAIL
                Util_Email ue = UtilEmailManager.ReturnUtilEmailByEmail(email);

                //Hachage MdP
                byte[] data = System.Text.Encoding.ASCII.GetBytes(mdp);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                String mdpHash = System.Text.Encoding.Default.GetString(data);

                //INSERTION BDD UTIL
                Util u = new Util
                {
                    dCrea = DateTime.Now,
                    id_Declinaison_Culture = 1,
                    idu_Email = ue.idu_Email,
                    mdp = mdpHash,
                    uid = DefaultValueManager.ReturnSQLGuid()
                };
                UtilManager.InsertLine(u, true);
                SessionManager.Current.Util.Util_Email = ue;

                //Insertion Consentement
                UtilConsentementManager.InsertConsentement(null, u.IdU, 1, 0);
                UtilConsentementManager.InsertConsentement(null, u.IdU, 2, 1);

                //Envoi de l'email de confirmaion
                MailBase mb = new MailBase("Confirmez votre compte", "Voir ce mail sur ordinateur ?", "InterBook", 1, SessionManager.Current.Util);
                MailManager.SendMail(mb);

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
        public ActionResult Criterions()
        {
            return View(new CriterionsModel());
        }

        //
        // POST: Account/Criterions
        [HttpPost]
        public ActionResult Criterions(CriterionsModel cm)
        {
            if (ModelState.IsValid)
            {
                //INSERTION BDD UTIL_PROFESSION
                //INSERTION BDD UTIL_VILLE
                //INSERTION BDD UTIL_POSTAL
                //INSERTION BDD UTIL
                return this.SetPageRedirect("Dashboard", "Index");
            }
            return View(cm);
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
        public JsonResult SignIn(string email, string mdp)
        {
            //Hachage MdP
            byte[] data = System.Text.Encoding.ASCII.GetBytes(mdp);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String mdpHash = System.Text.Encoding.Default.GetString(data);

            Util util = UtilManager.GetUtilByEmailMdp(email, mdpHash);
            if (util == null)
            {
                return Json(new { Success = true, Reponse = "0", Message = "inconnu" });
            }
            SessionManager.Current.Util = util;

            if (util.Util_Postal != null)
                return Json(new { Success = true, Reponse = "1", Message = util.Util_Postal.prenom });
            else
                return Json(new { Success = true, Reponse = "2", Message = email });
        }
    }
}
