using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilPostalManager
    {
        public static void InsertLine(Util_PostalSimple utilpostal)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine_Util_Postal(utilpostal);
        }

        public static Ref_VilleSimple GetRefVilleByDescription(string desc)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetVilleByNom(desc);
        }

        public static Util_PostalSimple GetUtilPostalByIdu(int idue)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilPostalByIdu(idue);
        }

        public static Ref_VilleSimple GetRefVilleByIduPostal(int idup)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetRefVilleByIduPostal(idup);
        }
    }
}