using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilInfoManager
    {
        public static void InsertLine(Util_InfoSimple UtilInfo)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine_Util_Info(UtilInfo);
        }

        public static void InsertLine(Util_Info_EntrepriseSimple UtilInfoEntreprise)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine_Util_Info_Entreprise(UtilInfoEntreprise);
        }

        public static Util_InfoSimple GetUtilInfoByIdu(int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilInfoByIdu(idu);
        }

        public static Util_Info_EntrepriseSimple GetUtilInfoEntrepriseByIdu(int idue)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilInfoEntrepriseByIdu(idue);
        }
    }
}