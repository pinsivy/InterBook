using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public static class SessionManager
    {
        public static SessionObject Current
        {
            get
            {
                if (HttpContext.Current.Session["MaSession"] == null)
                    HttpContext.Current.Session["MaSession"] = new SessionObject();

                return HttpContext.Current.Session["MaSession"] as SessionObject;
            }
        }

        public static void ClearSession()
        {
            if (HttpContext.Current.Session["MaSession"] != null)
                HttpContext.Current.Session["MaSession"] = null;
        }
    }
}