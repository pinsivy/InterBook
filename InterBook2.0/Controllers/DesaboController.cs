using InterBook2._0.BLL;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Controllers
{
    public class DesaboController : Controller
    {
        public ActionResult Index(DesaboModel model)
        {
            if (model.IsValid())
            {
                Util util = UtilManager.GetUtilByUid(model.Uid.Value);
                if (util != null && util.IdU > 0)
                {
                    if (model.clic == 0)
                    {
                        string link = string.Format("{0}?uid={1}&idm={2}&clic=1", this.Request.Path, model.Uid, model.Idm);
                        model.HtmlContent = string.Format(App_GlobalResources.Commun.OptoutInsertConfirm, link);
                        UtilConsentementManager.InsertConsentement(model.Idm, util.IdU, 2, 0);
                    }
                    else
                    {
                        UtilConsentementManager.InsertConsentement(model.Idm, util.IdU, 2, 1);
                        model.HtmlContent = App_GlobalResources.Commun.OptoutDeleteConfirm;
                    }
                }
            }

            return View(model);
        }

    }
}
