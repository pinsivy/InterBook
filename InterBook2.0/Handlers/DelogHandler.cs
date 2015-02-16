using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;

namespace Website.Handlers
{
    public class DelogHandler : IHttpHandler, IRequiresSessionState
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            if (context.Session != null)
                context.Session.Clear();

            context.Response.Redirect("/");
        }

        #endregion
    }

    public class DelogRouteHandler : IRouteHandler
    {
        #region IRouteHandler Members

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new DelogHandler();
        }

        #endregion
    }
}