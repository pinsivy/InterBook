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

            // le contrôleur et l'action demandé
            string currentCtrl = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string currentAction = filterContext.ActionDescriptor.ActionName.ToLower();

            UrlHelper helper = new UrlHelper(filterContext.RequestContext);

            string sessionControllerAction = SessionManager.Current.CurrentPage;

            //////////////////////////////
            // RETIRER
            //////////////////////////////
            if (false)
            {
                // Il y a rien en session
                if (string.IsNullOrEmpty(sessionControllerAction))
                {
                    // On ajoute le contrôleur et l'action demandé dans la variable contenant la session
                    sessionControllerAction = string.Format("{0}/{1}", currentCtrl, currentAction);
                }
                else
                {
                    string[] actionCtrl = sessionControllerAction.Split('/');
                    string sessionCtrl = actionCtrl[0].ToLower();
                    string sessionAction = actionCtrl[1].ToLower();

                    // La demande courante est différente de ce qu'il y a en session
                    if (currentCtrl != sessionCtrl)
                    {
                        filterContext.Result = new RedirectResult(helper.Action(sessionAction, sessionCtrl));
                        return;
                    }
                }

                // Gestion des back
                Back back = BaseManager.HandleBacks(sessionControllerAction.ToLower());
                if (back != null)
                {
                    filterContext.Result = new RedirectResult(helper.Action(back.Action, back.Controller));
                    return;
                }
            }
        }

        protected RedirectToRouteResult SetPageRedirect(string controller, string action)
        {
            SessionManager.Current.CurrentPage = string.Format("{0}/{1}", controller, action);
            return RedirectToAction(action, controller);
        }
    }
}
