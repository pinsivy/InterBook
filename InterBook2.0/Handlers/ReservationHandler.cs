using InterBook2._0.BLL;
using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using Website.Handlers;

namespace InterBook2._0.Handlers
{
    public class ReservationHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //Variable assigné à false par défaut
            bool logOK = false;
            Util u = new Util();

            if (!string.IsNullOrEmpty(HttpContext.Current.Request["uid"]))
            {

                //récupération des infos sur l'idu
                u = UtilManager.GetUtilByUid(new Guid(HttpContext.Current.Request["uid"]));

                if (u.IdU != 0)
                {
                    logOK = true;
                }

                //si on a réussi à se logguer
                if (logOK && !string.IsNullOrEmpty(HttpContext.Current.Request["idr"]) && !string.IsNullOrEmpty(HttpContext.Current.Request["act"]))
                {

                    //on recronstruit l'objet session
                    SessionManager.BuildSession(u.Util_Email.email);

                    //check si il y a vraiment une réservation pour cet idu
                    ReservationSimple r = ReservationManager.GetReservationByIdr(int.Parse(HttpContext.Current.Request["idr"]));
                    if(r.idUEmploye == SessionManager.Current.Util.IdU)
                    {
                        Util_PostalSimple ups = UtilPostalManager.GetUtilPostalByIdu((int)r.idUEntreprise);
                        Util_Info_EntrepriseSimple uies = UtilInfoManager.GetUtilInfoEntrepriseByIdu((int)r.idUEntreprise);
                        
                        if (HttpContext.Current.Request["act"] == "0" && r.id_EtatReservation == 1)
                        {
                            r.id_EtatReservation = 3;
                            ReservationManager.InsertLine(r);
                            SessionManager.Current.Notification = "Vous avez refusé que " + ups.prenom + " " + ups.nom + " (" + uies.Nom + ") puisse vous parler.";
                        }
                        else if (HttpContext.Current.Request["act"] == "1" && r.id_EtatReservation == 1)
                        {
                            //Reservation
                            r.id_EtatReservation = 2;
                            ReservationManager.InsertLine(r);
                            //Util_Contact
                            Util_ContactSimple uc = new Util_ContactSimple(){iduFrom = r.idUEntreprise,iduTo = r.idUEmploye};
                            Util_ContactSimple uc2 = new Util_ContactSimple(){iduFrom = r.idUEmploye,iduTo = r.idUEntreprise};
                            UtilContactManager.InsertLine(uc);
                            UtilContactManager.InsertLine(uc2);
                            //Util_Dispo
                            DateTime dt = (DateTime)r.debut;
                            do
                            {
                                UtilDispoManager.ajoutReserve(dt, (int)r.idUEmploye);
                                dt = dt.AddDays(1);
                            }
                            while (dt <= (DateTime)r.fin);
                            SessionManager.Current.Notification = ups.prenom + " " + ups.nom + " (" + uies.Nom + ") peut maintenant vous envoyer un message.";
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("/");
                        }


                    }

                    //redirection sur la bonne page
                    HttpContext.Current.Response.Redirect("Dashboard/Messages");
                }
                else
                {
                    HttpContext.Current.Response.Redirect("/");
                }
            }
        }
    }


    public class ReservationRouteHandler : IRouteHandler
    {
        #region IRouteHandler Members

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new ReservationHandler();
        }

        #endregion
    }
}