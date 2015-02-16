using System;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Website.Models;
using InterBook2._0.BLL;

namespace Website.Controllers
{
    public class ShowSessionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ShowSessionModel model = new ShowSessionModel(HttpContext);
            return View(model);
        }
    }
}
