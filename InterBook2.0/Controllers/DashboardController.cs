using InterBook2._0.BLL;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace InterBook2._0.Controllers
{
    [Serializable]
    public class DashboardController : BaseController
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            DashboardModel dm = new DashboardModel();


            return View(dm);
        }

        //
        // POST: /Dashboard/ajoutDispo
        [HttpPost]
        public JsonResult ajoutDispo(DateTime dDispo)
        {
            Util_Dispo ud = UtilDispoManager.ajoutDispo(dDispo, 3);
            return Json(new { Success = true, Reponse = ud, Message = ud.id_Ref_Dispo });
        }

        //
        // GET: /Dashboard/recupDispo
        [HttpPost]
        public JsonResult RecupDispo()
        {
            List<Util_Dispo> ud = UtilDispoManager.GetUtilDispoByIdu(3007);
            if (ud == null)
            {
                return Json(null);
            }

            return Json(ud, JsonRequestBehavior.AllowGet);
            //return Json(new { Success = true, Reponse = new JavaScriptSerializer().Serialize(ud), Message = "lol" });
        }
    }
}
