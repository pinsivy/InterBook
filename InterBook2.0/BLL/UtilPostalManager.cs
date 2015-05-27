using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilPostalManager
    {
        public static void InsertLine(Util_Postal utilpostal)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine(utilpostal);
        }

        public static Ref_Ville GetRefVilleByDescription(string desc)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetVilleByNom(desc);
        }
    }
}