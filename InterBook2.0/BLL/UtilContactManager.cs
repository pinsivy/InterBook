using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilContactManager
    {
        public static void InsertLine(Util_Contact UtilContact)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine(UtilContact);
        }
    }
}