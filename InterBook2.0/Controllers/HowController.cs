using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Controllers
{
    public class HowController : BaseController
    {
        //
        // GET: /How/

        public ActionResult Index()
        {
            HowModel hm = new HowModel();


            return View(hm);
        }

    }
}
