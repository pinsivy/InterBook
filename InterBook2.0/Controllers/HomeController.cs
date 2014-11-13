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
            //IBWS ws = new IBWS();
            //var u = ws.GetUtilByIdu(1);
            //if (u != null)
            //{
            //    u.idu_email++;
            //    ws.save();
            //}
            //BLL.SessionManager.Current.Util = u;

            return View(hm);
        }
    }
}
