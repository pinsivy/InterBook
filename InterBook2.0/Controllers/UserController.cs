using InterBook2._0.BLL;
using InterBook2._0.BLL.Mail;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            UserModel um = new UserModel();
            um.u = UtilManager.GetUtilByIdU(idu);
            return View(um);
        }

        // POST : User/Reserve
        [HttpPost]
        public JsonResult Reserve(string idu, string dDebutReserve, string dFinReserve)
        {
            if (SessionManager.Current.Util == null)
                return Json(new { Success = true, Reponse = "0", Message = "" });

            if (SessionManager.Current.Util.particulier == true)
                return Json(new { Success = true, Reponse = "1", Message = "" });

            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            DateTime dtDebut = new DateTime(), dtFin = new DateTime();
            if (!string.IsNullOrEmpty(dDebutReserve))
                DateTime.TryParse(dDebutReserve, out dtDebut);
            if (!string.IsNullOrEmpty(dFinReserve))
                DateTime.TryParse(dFinReserve, out dtFin);

            Reservation r = new Reservation{
                idUEmploye = int.Parse(idu),
                idUEntreprise = SessionManager.Current.Util != null ? SessionManager.Current.Util.IdU : 0,
                debut = dtDebut,
                fin = dtFin,
                id_EtatReservation = 1
            };

            SessionManager.Current.ws.InsertLine(r);
            SessionManager.Current.Util.Reservations.Add(r);

            //Envoi de l'email de reservation
            Util uTo = UtilManager.GetUtilByIdU(int.Parse(idu));
            MailBase mb = new MailBase(SessionManager.Current.Util.Util_Info_Entreprise.Nom + " souhaîte vous contacter sur InterBook.fr", "Voir ce mail sur ordinateur ?", SessionManager.Current.Util.Util_Info_Entreprise.Nom, SessionManager.Current.Util.Util_Info_Entreprise.Email, 2, SessionManager.Current.Util, uTo.Util_Email);
            Dictionary<String, String> varsAdd = new Dictionary<String, String> 
            {
                {"[[uidto]]", uTo.uid.ToString()},
                {"[[idr]]", r.id_Reservation.ToString()},
                {"[[nomEntreprise]]", SessionManager.Current.Util.Util_Info_Entreprise.Nom}
            };
            MailManager.SendMail(mb, varsAdd);

            return Json(new { Success = true, Reponse = "2", Message = ""});
        }

        // POST : User/Reserve
        [HttpPost]
        public JsonResult addFavori(string idu)
        {
            if (SessionManager.Current.Util == null)
                return Json(new { Success = true, Reponse = "0", Message = "" });

            if (SessionManager.Current.Util.particulier == true)
                return Json(new { Success = true, Reponse = "1", Message = "" });

            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            //voir si existe
            Util_Favoris uf = UtilFavorisManager.GetUtilFavorisByIduIduEnt(int.Parse(idu), SessionManager.Current.Util.IdU);
            
            //Si existe, changer actif
            if (uf != null)
                uf.actif = !uf.actif;
            //Sinon créer avec actif = 1
            else
            {
                uf = new Util_Favoris
                {
                    idU = int.Parse(idu),
                    idUEntreprise = SessionManager.Current.Util != null ? SessionManager.Current.Util.IdU : 45,
                    actif = true
                };
            }

            //insérer et mettre en session
            UtilFavorisManager.InsertLine(uf);
            SessionManager.Current.Util.Util_Favoris.Add(uf);

            if(uf.actif == true)
                return Json(new { Success = true, Reponse = "2", Message = "" });
            else
                return Json(new { Success = true, Reponse = "3", Message = "" });
        }
    }
}
