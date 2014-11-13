using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.BLL
{
    public class BaseManager
    {
        private static readonly List<string> AuthorizedBacks = new List<string>
        {
            "home/index",
            "account/signin",
            "signup/index",
            "user/index"
        };

        public static Back HandleBacks(string controlerAction)
        {
            Back homeBack = new Back { Action = "Index", Controller = "Home" };
            if (!AuthorizedBacks.Contains(controlerAction))
            {
                if (SessionManager.Current.Util == null)
                {
                    return homeBack;
                }
                else
                {
                    //si pas d'idu on redirige sur la home
                    if (SessionManager.Current.Util.IdU == 0)
                        return homeBack;

                    //gestion des backs sur la bonne page ou pas
                    if (SessionManager.Current.CurrentPage != null && controlerAction != SessionManager.Current.CurrentPage.ToLower())
                    {
                        return CreateBack(SessionManager.Current.CurrentPage);
                    }
                }
            }
            else if (SessionManager.Current.Util != null && SessionManager.Current.Util.IdU > 0)
            {
                return CreateBack(SessionManager.Current.CurrentPage);
            }

            return null;
        }

        private static Back CreateBack(string currentPage)
        {
            string[] splitted = currentPage.Split('/');
            return new Back
            {
                Action = splitted[1],
                Controller = splitted[0]
            };
        }

        
    }
    
    public class Back
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Url { get; set; }
    }
}
