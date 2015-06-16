using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilConsentementManager
    {
        public static List<Util_ConsentementSimple> GetUtilConsentementByIdu(int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilConsentementByIdu(idu);
        }

        public static void InsertConsentement(int? id_Mailing, int idu, int id_TypeConsentement, int valeur)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            Util_ConsentementSimple uc = SessionManager.Current.ws.GetUtilConsentementByIduIdm(idu, id_TypeConsentement);
            if(uc != null)
            {
                uc.id_Mailing = id_Mailing;
                uc.id_TypeConsentement = id_TypeConsentement;
                uc.idu = idu;
                uc.valeur = valeur;
            }
            else
            {
                uc = new Util_ConsentementSimple()
                {
                    id_Mailing = id_Mailing,
                    id_TypeConsentement = id_TypeConsentement,
                    idu = idu,
                    valeur = valeur,
                    dRecueil = DateTime.Now
                };
            }

            SessionManager.Current.ws.InsertLine_Util_Consentement(uc);
        }
    }
}