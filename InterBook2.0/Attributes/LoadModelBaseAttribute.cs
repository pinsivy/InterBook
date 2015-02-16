using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;


namespace Website.Attributes
{
    public class LoadModelBaseAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Controller.ViewData.Model == null)
                filterContext.Controller.ViewData.Model = new ModelBase();

            base.OnActionExecuted(filterContext);
        }
    }
}