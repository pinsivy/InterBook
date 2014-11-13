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
    public class UserController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int idu)
        {
            IBWS ws = new IBWS();
            UserModel um = new UserModel();
            um.u = ws.GetUtilByIdu(idu);
            return View(um);
        }

        // GET : User/Connecter
        [AllowAnonymous]
        public JsonResult Connecter(string email, string mdp)
        {
            Util util = UtilManager.GetUtilByEmailMdp(email, mdp);
            if (util == null)
            {
                return Json(new { Success = true, Message = "Cet association email / passwod est incorrect." });
            }
            SessionManager.Current.Util = util;
            this.SetPageRedirect("Dashboard", "Index");
            return Json(new { Success = true, Message = "/Dashboard" });
        }
    }
}
