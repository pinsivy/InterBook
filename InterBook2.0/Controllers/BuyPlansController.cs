using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Controllers
{
    public class BuyPlansController : BaseController
    {
        //
        // GET: /BuyPlans/

        public ActionResult Index()
        {
            BuyPlansModel bpm = new BuyPlansModel();


            return View(bpm);
        }
    }
}