using InterBook2._0.BLL;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: Account/SignIn
        [HttpGet]
        public ActionResult SignIn()
        {
            return View(new SignInModel());
        }

        //
        // POST: Account/SignIn
        [HttpPost]
        public ActionResult SignIn(SignInModel model)
        {
            //ALLER SUR /Dashboard
            if (ModelState.IsValid)
            {
                ////INSERTION BDD UTIL_EMAIL
                Util_Email ue = UtilEmailManager.ReturnUtilEmailByEmail(model.Email);

                ////INSERTION BDD UTIL_POSTAL
                Util_Postal up = new Util_Postal
                {
                    dCrea = DateTime.Now,
                    id_Civilite = model.Civilite,
                    nom = Capitalize(model.Nom),
                    prenom = Capitalize(model.Prenom)
                };
                UtilPostalManager.InsertLine(up);

                //INSERTION BDD UTIL
                Util u = new Util
                {
                    dCrea = DateTime.Now,
                    id_Declinaison_Culture = 1,
                    idu_Email = ue.idu_Email,
                    idu_Postal = up.idu_Postal,
                    mdp = model.Password
                };
                UtilManager.InsertLine(u, true);

                return this.SetPageRedirect("Account", "Criterions");
            }

            return View(model);
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

        //
        // GET: Account/SignUp
        [HttpGet]
        public ActionResult SignUp()
        {
            SignUpModel cm = new SignUpModel();
            return View(cm);
        }

        //
        // POST: Account/SignUp
        [HttpPost]
        public ActionResult SignUp(SignUpModel cm)
        {
            if (ModelState.IsValid)
            {
                //INSERTION BDD UTIL_EMAIL
                //INSERTION BDD UTIL_POSTAL
                //INSERTION BDD UTIL
                return this.SetPageRedirect("Dashboard", "Index");
            }
            return View(cm);
        }
    }
}
