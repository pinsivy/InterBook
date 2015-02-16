using InterBook2._0.BLL;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
            //IBWS ws = new IBWS();

            return View(model);
        }

        //
        // Get: /Search/Filtre
        [AllowAnonymous]
        public String Filtre(string ville, DateTime? debut, DateTime? fin, string profession, string experience, string contrat)
        {
            IBWS ws = new IBWS();
            string json = ws.GetUtilsByVilleProfessionExperienceContrat(ville, profession, experience, contrat);
            List<Util> la = JsonConvert.DeserializeObject<List<Util>>(json);
            la = la;
            return json;
        }


    }
}
