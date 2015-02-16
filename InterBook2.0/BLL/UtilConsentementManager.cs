using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilConsentementManager
    {
        public static List<Util_Consentement> GetUtilConsentementByIdu(int idu)
        {
            IBWS ws = new IBWS();
            return ws.GetUtilConsentementByIdu(idu);
        }

        public static void InsertConsentement(int? id_Mailing, int idu, int id_TypeConsentement, int valeur)
        {
            IBWS ws = new IBWS();
            Util_Consentement uc = ws.GetUtilConsentementByIduIdm(idu, id_TypeConsentement);
            if(uc != null)
            {
                uc.id_Mailing = id_Mailing;
                uc.id_TypeConsentement = id_TypeConsentement;
                uc.idu = idu;
                uc.valeur = valeur;
            }
            else
            {
                uc = new Util_Consentement()
                {
                    id_Mailing = id_Mailing,
                    id_TypeConsentement = id_TypeConsentement,
                    idu = idu,
                    valeur = valeur,
                    dRecueil = DateTime.Now
                };
            }

            ws.InsertLine(uc);
        }
    }
}