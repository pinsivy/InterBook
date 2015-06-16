using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilEmailManager
    {
        public static Util_EmailSimple ReturnUtilEmailByEmail(string email)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.ReturnUtilEmailByEmail(email);
        }

        public static Util_EmailSimple GetUtilEmailByEmail(string email, bool particulier)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilEmailByEmail(email, particulier);
        }

        public static Util_EmailSimple GetUtilEmailByIduEmail(int IduEmail)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilEmailByIduEmail(IduEmail);
        }

        public static void InsertLine(Util_EmailSimple utilemail)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine_Util_Email(utilemail);
        }
    }
}