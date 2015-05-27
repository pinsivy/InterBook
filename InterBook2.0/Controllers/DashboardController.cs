using AuthorizeNet;
using InterBook2._0.BLL;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using Newtonsoft.Json;
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
        // GET: /Dashboard/Disponibility

        public ActionResult Disponibility()
        {
            DashboardModel dm = new DashboardModel();

            dm.u = SessionManager.Current.Util;

            return View(dm);
        }

        //
        // GET: /Dashboard/Messages
        public ActionResult Messages()
        {
            DashboardModel dm = new DashboardModel();


            //Recupérer contact
            dm.luc = UtilMessageManager.GetUtilContactByIdufrom(SessionManager.Current.Util.IdU);

            return View(dm);
        }

        //
        // GET: /Dashboard/Favoris
        public ActionResult Favoris()
        {
            DashboardModel dm = new DashboardModel();

            dm.u = SessionManager.Current.Util;

            return View(dm);
        }

        //
        // GET: /Dashboard/Premium
        public ActionResult Premium()
        {
            String ApiLogin = "7JDYGj99dKy";
            String TransactionKey = "889jY6Du4szCEj3U";
            String checkoutform = SIMFormGenerator.OpenForm(ApiLogin,TransactionKey, 2.25M, "", true);

            checkoutform = checkoutform + "<input type='submit' class='submit' value='Order with SIM!' />";

            checkoutform = checkoutform + SIMFormGenerator.EndForm();
            ViewBag.contentHtmlCB = checkoutform;

            DashboardModel dm = new DashboardModel();

            dm.u = SessionManager.Current.Util;

            return View(dm);
        }

        //
        // GET: /Dashboard/ModifProfil
        public ActionResult ModifProfil()
        {
            DashboardModel dm = new DashboardModel();

            dm.u = SessionManager.Current.Util;

            return View(dm);
        }

        //
        // POST: /Dashboard/ajoutDispo
        [HttpPost]
        public JsonResult ajoutDispo(DateTime dDispo)
        {
            Util_DispoSimple ud = UtilDispoManager.ajoutDispo(dDispo, SessionManager.Current.Util.IdU);

            string jsonud = JsonConvert.SerializeObject(ud, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return Json(new { Success = true, Reponse = jsonud, Message = ud.id_Ref_Dispo });
        }

        //
        // GET: /Dashboard/recupDispo
        [HttpGet]
        public JsonResult RecupDispo(int idu)
        {
            List<Util_DispoSimple> ud = UtilDispoManager.GetUtilDispoByIdu(idu);
            if (ud == null)
            {
                return Json(null);
            }

            string jsonud = JsonConvert.SerializeObject(ud, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Json(jsonud, JsonRequestBehavior.AllowGet);
            //return Json(new { Success = true, Reponse = new JavaScriptSerializer().Serialize(ud), Message = "lol" });
        }

        //
        // GET: /Dashboard/getMessage
        [HttpGet]
        public JsonResult getMessage(int iduTo)
        {
            List<Util_MessageSimple> lum = UtilMessageManager.GetUtilMessageByIdutoIdufrom(SessionManager.Current.Util.IdU, iduTo);
            if (lum == null)
            {
                return Json(null);
            }

            string jsonud = JsonConvert.SerializeObject(lum, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Json(jsonud, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Dashboard/addMessage
        [HttpPost]
        public JsonResult addMessage(string iduTo, string message)
        {
            Util_Message um = UtilMessageManager.addMessage(SessionManager.Current.Util.IdU, int.Parse(iduTo), message);
            return Json(new { Success = true, Reponse = um, Message = "" });
        }
    }
}
