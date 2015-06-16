using InterBook2._0.BLL;
using InterBook2._0.DTO;
using InterBook2._0.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
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
        public ActionResult Index(SearchModel model, string exp)
        {
            if (model == null)
                model = new SearchModel();

            //Recherche dans l'API
            //if (SessionManager.Current.ws == null)
            SessionManager.Current.ws = new IBWS();

            //Recupère la ville en BDD
            Ref_VilleSimple rv = null;
            if (model.Ville != null)
            {
                String ville = model.Ville.Split(new string[] { " (" }, StringSplitOptions.None)[0];
                rv = UtilPostalManager.GetRefVilleByDescription(ville);
            }

            if (model.Debut == DateTime.MinValue && model.Fin != DateTime.MinValue)
                model.Debut = DateTime.Now;
            else if (model.Debut != DateTime.MinValue && model.Fin == DateTime.MinValue)
                model.Fin = model.Debut;
            else if (model.Debut == DateTime.MinValue && model.Fin == DateTime.MinValue)
                model.Debut = model.Fin = DateTime.Now;

            //Recupère les Util avec le rayon
            List<UtilSimple> la = null;
            if (rv != null)
            {
                if (!String.IsNullOrEmpty(exp))
                    la = SessionManager.Current.ws.GetUtilSearchByLongLatIddepRayonDateAndParam(double.Parse(rv.longitude.Replace(".", ",")), double.Parse(rv.latitude.Replace(".", ",")), rv.id_Departement, 2000, model.Debut, model.Fin, exp, null);//RAYON
                else
                    la = SessionManager.Current.ws.GetUtilSearchByLongLatIddepRayonDate(double.Parse(rv.longitude.Replace(".", ",")), double.Parse(rv.latitude.Replace(".", ",")), rv.id_Departement, 2000, model.Debut, model.Fin);//RAYON
            }

            if (la != null)
            {
                model.luSearch = new List<UtilSearch>();
                UtilSearch us = null;
                foreach (UtilSimple u in la)
                {
                    us = new UtilSearch();
                    us.uSearch = u;
                    Ref_VilleSimple rvs = new Ref_VilleSimple();
                    if (u.idu_Postal != null)
                    {
                        us.uInfoSearch = UtilInfoManager.GetUtilInfoByIdu((int)u.IdU);
                        us.uPostalSearch = UtilPostalManager.GetUtilPostalByIdu((int)u.IdU);
                        us.uVilleSearch = UtilPostalManager.GetRefVilleByIduPostal((int)u.idu_Postal);
                    }
                    if (rv != null && us.uVilleSearch != null)
                    {
                        us.dist = distance(double.Parse(rv.latitude.Replace(".", ",")), double.Parse(rv.longitude.Replace(".", ",")), double.Parse(us.uVilleSearch.latitude.Replace(".", ",")), double.Parse(us.uVilleSearch.longitude.Replace(".", ",")));
                        model.luSearch.Add(us);
                    }
                }
            }

            return View(model);
        }

        ////
        //// Get: /Search/Filtre
        //[AllowAnonymous]
        //public String Filtre(string ville, string idVille, DateTime? debut, DateTime? fin, string profession, string experience, string contrat)
        //{
        //    if (SessionManager.Current.ws == null)
        //        SessionManager.Current.ws = new IBWS();
        //    string json = SessionManager.Current.ws.GetUtilsByVilleProfessionExperienceContrat(ville, profession, experience, contrat);
        //    Ref_Ville rv = SessionManager.Current.ws.GetVille(int.Parse(idVille == null ? "0" : idVille));
        //    List<Util> la = JsonConvert.DeserializeObject<List<Util>>(json);
        //    return json;
        //}

        private double distance(double lat1, double lon1, double lat2, double lon2) {
          double theta = lon1 - lon2;
          double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
          dist = Math.Acos(dist);
          dist = rad2deg(dist);
          dist = dist * 60 * 1.1515;
          dist = dist * 1.609344;
          return (dist);
        }
        private double deg2rad(double deg) {
          return (deg * Math.PI / 180.0);
        }
        private double rad2deg(double rad) {
          return (rad / Math.PI * 180.0);
        }

    }
}
