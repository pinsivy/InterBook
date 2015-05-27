using InterBook2._0.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DeclinaisonCultureManager.SetCulture();

            // le contrôleur et l'action demandé
            string currentCtrl = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string currentAction = filterContext.ActionDescriptor.ActionName.ToLower();

            UrlHelper helper = new UrlHelper(filterContext.RequestContext);

            string sessionControllerAction = string.Format("{0}/{1}", currentCtrl, currentAction);

            // Gestion des back
            Back back = BaseManager.HandleBacks(sessionControllerAction.ToLower());
            if (back != null)
            {
                filterContext.Result = new RedirectResult(helper.Action(back.Action, back.Controller));
                return;
            }
        }

        protected RedirectToRouteResult SetPageRedirect(string controller, string action)
        {
            SessionManager.Current.CurrentPage = string.Format("{0}/{1}", controller, action);
            return RedirectToAction(action, controller);
        }

        public ActionResult MenuProfil()
        {
            var rd = ControllerContext.ParentActionViewContext.RouteData;
            var currentAction = rd.GetRequiredString("action");
            var currentController = rd.GetRequiredString("controller");
            
            return View();
        }
    }
}
