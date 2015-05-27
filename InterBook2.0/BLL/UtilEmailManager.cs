using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilEmailManager
    {
        public static Util_Email ReturnUtilEmailByEmail(string email)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.ReturnUtilEmailByEmail(email);
        }

        public static Util_Email GetUtilEmailByEmail(string email, bool particulier)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilEmailByEmail(email, particulier);
        }

        public static void InsertLine(Util_Email utilemail)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine(utilemail);
        }
    }
}