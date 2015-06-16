using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilProfessionManager
    {
        public static void InsertLine(Util_ProfessionSimple UtilProfession)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine_Util_Profession(UtilProfession);
        }

        public static Ref_ProfessionSimple GetRefProfessionByDescription(string desc)
        {
            return SessionManager.Current.ws.GetRefProfessionByDescription(desc);
        }
    }
}