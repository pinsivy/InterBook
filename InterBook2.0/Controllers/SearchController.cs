using InterBook2._0.BLL;
using InterBook2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Controllers
{
    public class SearchController : BaseController
    {
        //
        // Get: /Search/
        [HttpGet]
        public ActionResult Index(SearchModel model)
        {
            if (model == null)
                model = new SearchModel();

            //Recherche dans l'API
            IBWS petitExemple = new IBWS();
            model.lu = petitExemple.GetUtilsByVilleProfession("Paris", "réalisateur");

            return View(model);
        }
    }
}
