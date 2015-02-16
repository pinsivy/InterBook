﻿using InterBook2._0.BLL;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using System;
using System.Collections.Generic;
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

        // POST: Home/GetProfessions
        [HttpGet]
        public JsonResult GetProfessions(string debut, string maxRows)
        {
            List<Ref_Profession> rps = UtilManager.GetProfessions(debut, maxRows);
            if (rps == null)
            {
                return Json(null);
            }

            return Json(rps, JsonRequestBehavior.AllowGet);
        }
    }
}
