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
        InterBookEntities _db;

        public HomeController()
        {
            _db = new InterBookEntities();
        }

        [HttpGet]
        public ActionResult Index(RequestModel model)
        {
            //var u = (from m in _db.Util where m.IdU == 1 select m).FirstOrDefault();
            //var u = BLL.UtilManager.GetUtilByIdU(1);
            HomeModel hm = new HomeModel();
            IBWS petitExemple = new IBWS();
            var u = petitExemple.GetUtilByIdu(1);
            if (u != null)
            {
                u.idu_email++;
                petitExemple.save();
            }

            BLL.SessionManager.Current.Util = u;

            return View(hm);
        }
    }
}
