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
            um.u = UtilManager.GetCompletUtilByIdu(idu);

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

            ReservationSimple rs = new ReservationSimple();

            SessionManager.Current.ws.InsertLine_Reservation(rs.id_Reservation.ToString(), rs.idUEntreprise.ToString(), rs.debut.ToString(), rs.fin.ToString(), rs.id_EtatReservation.ToString(), rs.idUEmploye.ToString());
            SessionManager.Current.Util.Reservations.Add(r);

            //Envoi de l'email de reservation
            UtilSimple uTo = UtilManager.GetUtilByIdU(int.Parse(idu));
            Util_EmailSimple uesTo = UtilEmailManager.GetUtilEmailByIduEmail(int.Parse(idu));
            Util_Email ueTo = new Util_Email
            {
                idu_Email = uesTo.idu_Email,
                email = uesTo.email,
                dCrea = uesTo.dCrea
            };
            MailBase mb = new MailBase(SessionManager.Current.Util.Util_Info_Entreprise.Nom + " souhaîte vous contacter sur InterBook.fr", "Voir ce mail sur ordinateur ?", SessionManager.Current.Util.Util_Info_Entreprise.Nom, SessionManager.Current.Util.Util_Info_Entreprise.Email, 2, SessionManager.Current.Util, ueTo);
            Dictionary<String, String> varsAdd = new Dictionary<String, String> 
            {
                {"[[uidto]]", uTo.uid.ToString()},
                {"[[idr]]", r.id_Reservation.ToString()},
                {"[[nomEntreprise]]", SessionManager.Current.Util.Util_Info_Entreprise.Nom},
                {"[[debut]]", rs.debut.ToString()},
                {"[[fin]]", rs.fin.ToString()}
            };
            MailManager.SendMail(mb, varsAdd);

            //Envoi Notification Android
            List<Util_AndroidSimple> uas = UtilManager.GetUtilAndroidByIdu(uTo.IdU);
            foreach (Util_AndroidSimple ua in uas)
            {
                NotificationManager.SendNotification(ua.registerid, "InterBook", SessionManager.Current.Util.Util_Info_Entreprise.Nom + " souhaîte vous contacter", "data.nomEntreprise=" + SessionManager.Current.Util.Util_Info_Entreprise.Nom + "&data.idr=" + r.id_Reservation.ToString() + "&data.debut=" + r.debut.ToString() + "&data.fin=" + r.fin.ToString());
            }

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
            Util_FavorisSimple ufs = UtilFavorisManager.GetUtilFavorisByIduIduEnt(int.Parse(idu), SessionManager.Current.Util.IdU);
            
            //Si existe, changer actif
            if (ufs != null)
                ufs.actif = !ufs.actif;
            //Sinon créer avec actif = 1
            else
            {
                ufs = new Util_FavorisSimple
                {
                    idU = int.Parse(idu),
                    idUEntreprise = SessionManager.Current.Util != null ? SessionManager.Current.Util.IdU : 45,
                    actif = true
                };
            }

            //insérer et mettre en session
            UtilFavorisManager.InsertLine(ufs);
            //SessionManager.Current.Util.Util_Favoris.Add(ufs);

            if(ufs.actif == true)
                return Json(new { Success = true, Reponse = "2", Message = "" });
            else
                return Json(new { Success = true, Reponse = "3", Message = "" });
        }
    }
}
