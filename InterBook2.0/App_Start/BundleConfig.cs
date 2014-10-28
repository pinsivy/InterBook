using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace InterBook2._0.App_Start
{
    public class BundleConfig
    {
        // Pour plus d'informations sur Bundling, accédez à l'adresse http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Media/js/base").Include(
                "~/Media/js/vendors/jquery-1.10.2.js",
                "~/Media/js/vendors/jquery-ui.js",
                "~/Media/js/vendors/knockout-2.3.0.js",
                "~/Media/js/vendors/jquery.unobtrusive*",
                "~/Media/js/vendors/jquery.validate*",
                "~/Media/js/vendors/jquery.validate.unobtrusive*",
                "~/Media/js/vendors/validators.js")
            );

            bundles.Add(new ScriptBundle("~/Media/js/front").Include(
                "~/Media/js/vendors/TweenMax*")
            );

            bundles.Add(new ScriptBundle("~/Media/js/modernizr").Include(
                "~/Media/js/vendors/modernizr-2.6.2-respond-1.1.0*")
            );

            bundles.Add(new StyleBundle("~/Media/css/base").Include(
                "~/Media/css/jquery-ui-1.10.3.custom.css",
                "~/Media/css/Site.css",
                "~/Media/css/normalize*")
            );

            bundles.Add(new StyleBundle("~/Media/css/home").Include(
                "~/Media/css/Home.css")
            );

            bundles.Add(new StyleBundle("~/Media/css/recherche").Include(
                "~/Media/css/Recherche.css")
            );

        }
    }
}