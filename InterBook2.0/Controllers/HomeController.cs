using InterBook2._0.BLL;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace InterBook2._0.Controllers
{
    [Serializable]
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index(RequestModel model)
        {
            HomeModel hm = new HomeModel();

            //METHODE 1
            //var u = (from m in _db.Util where m.IdU == 1 select m).FirstOrDefault();
            //var u = BLL.UtilManager.GetUtilByIdU(1);

            //METHODE 2
            //if (SessionManager.Current.ws == null)
            //SessionManager.Current.ws = new IBWS();
            //var u = ws.GetUtilByIdu(1);
            //if (u != null)
            //{
            //    u.idu_email++;
            //    ws.save();
            //}
            //BLL.SessionManager.Current.Util = u;
            SessionManager.SendNotification("", "Text IB4");
            return View(hm);
        }

        // AJAX: Home/GetProfessions
        [HttpGet]
        public JsonResult GetProfessions(string debut, string maxRows)
        {
            List<Ref_ProfessionSimple> rps = UtilManager.GetProfessions(debut, maxRows);
            if (rps == null)
            {
                return Json(null);
            }

            return Json(rps, JsonRequestBehavior.AllowGet);
        }

        // AJAX: Home/GetVilles
        [HttpGet]
        public JsonResult GetVilles(string debut, string maxRows)
        {
            List<Ref_VilleSimple> rvs = UtilManager.GetVilles(debut, maxRows);
            if (rvs == null)
                return Json(null);

            return Json(rvs, JsonRequestBehavior.AllowGet);
        }

        // AJAX: Home/GetVilles
        [HttpGet]
        public JsonResult GetVilleGeoloc(string latitude, string longitude, string distance)
        {
            Ref_VilleSimple rv = UtilManager.GetVilleGeoloc(double.Parse(latitude.Replace(".", ",")), double.Parse(longitude.Replace(".", ",")), int.Parse(distance));
            if (rv == null)
                return Json(null);

            return Json(rv, JsonRequestBehavior.AllowGet);
        }
    }
}
