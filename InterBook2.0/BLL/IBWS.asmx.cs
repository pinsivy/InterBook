using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace InterBook2._0.BLL
{
    /// <summary>
    /// Description résumée de IBWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class IBWS : System.Web.Services.WebService
    {

        InterBookEntities _db;

        [WebMethod]
        public void Save()
        {
            _db.SaveChanges();
        }

        [WebMethod]
        public Util GetUtilByIdu(int idu)
        {
            _db = new InterBookEntities();
            var u = (from m in _db.Utils where m.IdU == idu select m).FirstOrDefault();
            return u;
        }

        [WebMethod]
        public List<Util> GetUtilsByVilleProfession(string Ville, string Profession)
        {
            _db = new InterBookEntities();
            var us = _db.SearchByVilleProfession(Ville, Profession);
            List<Util> lu = null;
            if (us != null)
            {
                lu = new List<Util>();
                foreach (Util u in us)
                {
                    lu.Add(u);
                }
            }
            return lu;
        }


        [WebMethod]
        public List<Util> GetUtilsByVilleProfessionExperienceContrat(string Ville, string Profession, string Experience, string Contrat)
        {
            _db = new InterBookEntities();
            var us = _db.SearchByVilleProfessionExperienceContrat(Ville, Profession, Experience, Contrat);
            List<Util> lu = null;
            if (us != null)
            {
                lu = new List<Util>();
                foreach (Util u in us)
                {
                    lu.Add(u);
                }
            }
            return lu;
        }

        [WebMethod]
        public void InsertLine(Util u)
        {
            _db = new InterBookEntities();
            _db.Utils.Add(u);
            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Postal up)
        {
            _db = new InterBookEntities();
            _db.Util_Postal.Add(up);
            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Email ue)
        {
            _db = new InterBookEntities();
            _db.Util_Email.Add(ue);
            Save();
        }

        [WebMethod]
        public Util_Email ReturnUtilEmailByEmail(string email)
        {
            _db = new InterBookEntities();
            var ue = _db.SetUtilEmail(email);

            if (ue != null)
            {
                return ue.FirstOrDefault();
            }
            return null;
        }

        [WebMethod]
        public Util GetUtilByEmailMdp(string email, string mdp)
        {
            _db = new InterBookEntities();
            var u = (from m in _db.Utils
                     join e in _db.Util_Email on m.idu_Email equals e.idu_Email
                     where m.mdp == mdp && e.email == email
                     select m).FirstOrDefault();
            if (u != null)
                return u;
            return null;
        }
    }
}
