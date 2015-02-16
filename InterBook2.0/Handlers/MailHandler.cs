using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using Website.Handlers;

namespace InterBook2._0.Handlers
{
    public class MailHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            NameValueCollection nvc = context.Request.QueryString;

            // Récupération de l'idm
            int idm;
            if (!int.TryParse(nvc["idm"], out idm))
            {
                context.Response.Redirect("/");
                return;
            }

            // Vérification de l'existence du fichier html
            String filePath = HttpContext.Current.Server.MapPath("/media/mail/" + string.Format("{0:0000}", idm) + ".html");
            if (String.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                context.Response.Redirect("/");
                return;
            }

            // Chargement du fichier html
            StringBuilder htmlMail = new StringBuilder();
            using (StreamReader sr = new StreamReader(filePath))
            {
                htmlMail.Append(sr.ReadToEnd());
            }


            // Remplacement des persos
            if (nvc["prenomurl"] != null)
                htmlMail = htmlMail.Replace("[[prenom]]", HttpUtility.HtmlEncode(nvc["prenomurl"]));
            if (nvc["nomurl"] != null)
                htmlMail = htmlMail.Replace("[[nom]]", HttpUtility.HtmlEncode(nvc["nomurl"]));
            if (nvc["serverurl"] != null)
                htmlMail = htmlMail.Replace("[[server]]", HttpUtility.HtmlEncode(nvc["serverurl"]));

            // Remplacement des variables envoyées en get
            foreach (String variable in nvc)
                htmlMail = htmlMail.Replace("[[" + variable + "]]", HttpUtility.HtmlEncode(nvc[variable]));

            // Remplacement du lien miroir
            htmlMail = htmlMail.Replace("[[lienmiroir]]", "");

            //a Affichage du rendu final
            context.Response.Write(htmlMail);

        }
    }
        

    public class MailRouteHandler : IRouteHandler
    {
        #region IRouteHandler Members

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MailHandler();
        }

        #endregion
    }
}