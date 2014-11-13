using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Controllers
{
    public class DashboardController : BaseController
    {
        //
        // GET: /SignIn/

        public ActionResult Index()
        {
            DashboardModel dm = new DashboardModel();

            return View(dm);
        }

    }
}
