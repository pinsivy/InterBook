using InterBook2._0.BLL;
using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;

namespace Website.Handlers
{
    public class IdentifAutoHandler : IHttpHandler, IRequiresSessionState
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(SessionManager.Current.CurrentPage) && !string.IsNullOrEmpty(HttpContext.Current.Request["uid"]))
            {
                if ((HttpContext.Current.Request["uid"]).ToLower() != SessionManager.Current.Util.uid.ToString().ToLower())
                {
                    SessionManager.ClearSession();
                }
                else
                {
                    HttpContext.Current.Response.Redirect(SessionManager.Current.CurrentPage);
                }
            }


            //Variable assigné à false par défaut
            bool logOK = false;
            Util u = new Util();

            if (!string.IsNullOrEmpty(HttpContext.Current.Request["uid"]))
            {

                //récupération des infos sur l'idu
                u = UtilManager.GetUtilByUid(new Guid(HttpContext.Current.Request["uid"]));

                if (u.IdU != 0)
                {
                    logOK = true;
                }

                //si on a réussi à se logguer
                if (logOK)
                {

                    //on recronstruit l'objet session
                    SessionManager.BuildSession(u.Util_Email.email);

                    //redirection sur la bonne page
                    HttpContext.Current.Response.Redirect(SessionManager.Current.CurrentPage);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("/");
                }
            }
        }

        #endregion
    }

    public class IdentifAutoRouteHandler : IRouteHandler
    {
        #region IRouteHandler Members

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new IdentifAutoHandler();
        }

        #endregion
    }
}