using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public static class SessionManager
    {
        public static SessionObject Current
        {
            get
            {
                if (HttpContext.Current.Session["MaSession"] == null)
                    HttpContext.Current.Session["MaSession"] = new SessionObject();

                return HttpContext.Current.Session["MaSession"] as SessionObject;
            }
        }

        public static void ClearSession()
        {
            if (HttpContext.Current.Session["MaSession"] != null)
                HttpContext.Current.Session["MaSession"] = null;
        }

        public static void BuildSession(string email)
        {
            // Création de l'utilisateur
            SessionManager.Current.Util = UtilManager.GetUtilByEmail(email);

            // Si l'utilisateur n'existe pas, on le vire.
            if (!SessionManager.Current.UtilExists())
                return;

            int idu = SessionManager.Current.Util.IdU;

            // Création des optins
            List<Util_Consentement> consentements = UtilConsentementManager.GetUtilConsentementByIdu(idu);

        }
    }
}