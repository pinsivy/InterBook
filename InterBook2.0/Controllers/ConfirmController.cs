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
    public class ConfirmController : Controller
    {
        public ActionResult Index(ConfirmModel model)
        {
            if (model.IsValid())
            {
                Util util = UtilManager.GetUtilByUid(model.Uid.Value);
                if (util != null && util.IdU > 0)
                {
                    UtilConsentementManager.InsertConsentement(model.Idm, util.IdU, 1, 1);
                }
            }

            return View(model);
        }
    }
}
