using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilInfoManager
    {
        public static void InsertLine(Util_Info UtilInfo)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine(UtilInfo);
        }

        public static void InsertLine(Util_Info_Entreprise UtilInfoEntreprise)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine(UtilInfoEntreprise);
        }
    }
}