using InterBook2._0.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Website.Handlers;

namespace InterBook2._0
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{*x}", new { x = @".*\.asmx(/.*)?" });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Search",
                url: "s/{action}/{id}",
                defaults: new { controller = "Search", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "User",
                url: "u/{idu}",
                defaults: new { controller = "User", action = "Index", idu = "" },
                constraints: new { id = "[0-9]*" }
            );

            routes.MapRoute(
                name: "Account",
                url: "Account/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Premium",
                url: "Premium/{action}/{id}",
                defaults: new { controller = "Premium", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Dashboard",
                url: "Dashboard/{action}/{id}",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );

            // Handlers
            //Si on ajoute, les formulaire POST pointe vers delog
            routes.Add("Delog", new Route("Delog", new DelogRouteHandler()));
            routes.Add("IdentifAuto", new Route("IdentifAuto", new IdentifAutoRouteHandler()));
            routes.Add("Mail", new Route("Mail", new MailRouteHandler()));
            routes.Add("Reservation", new Route("Reservation", new ReservationRouteHandler()));

            //Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //404
            routes.MapRoute(
                name: "404-PageNotFound",
                url: "{*url}",
                defaults: new { controller = "Error", action = "Http404" }
           );
        }
    }
}