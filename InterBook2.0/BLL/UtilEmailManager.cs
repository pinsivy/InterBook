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
            IBWS ws = new IBWS();
            return ws.ReturnUtilEmailByEmail(email);
        }

        public static Util_Email GetUtilEmailByEmail(string email)
        {
            IBWS ws = new IBWS();
            return ws.GetUtilEmailByEmail(email);
        }

        public static void InsertLine(Util_Email utilemail)
        {
            IBWS ws = new IBWS();
            ws.InsertLine(utilemail);
        }
    }
}