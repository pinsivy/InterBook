using InterBook2._0.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
        public Util GetUtilByUid(Guid? uid)
        {
            _db = new InterBookEntities();
            var u = (from m in _db.Utils where m.uid == uid select m).FirstOrDefault();
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
        public String GetUtilsByVilleProfessionExperienceContrat(string Ville, string Profession, string Experience, string Contrat)
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
            //return lu;
            return JsonConvert.SerializeObject(lu, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [WebMethod]
        public void InsertLine(Util u)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(u).State = u.IdU == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Postal up)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(up).State = up.idu_Postal == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Email ue)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(ue).State = ue.idu_Email == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Dispo ud)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(ud).State = ud.id_Util_Dispo == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Consentement uc)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(uc).State = uc.id_Util_Consentement == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
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
        public Util_Email GetUtilEmailByEmail(string email)
        {
            _db = new InterBookEntities();
            var ue = (from m in _db.Utils
                     join e in _db.Util_Email on m.idu_Email equals e.idu_Email
                     where e.email == email
                     select e).FirstOrDefault();

            if (ue != null)
                return ue;
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

        [WebMethod]
        public Util GetUtilByEmail(string email)
        {
            _db = new InterBookEntities();
            var u = (from m in _db.Utils
                     join e in _db.Util_Email on m.idu_Email equals e.idu_Email
                     where e.email == email
                     select m).FirstOrDefault();
            if (u != null)
                return u;
            return null;
        }

        [WebMethod]
        public List<Ref_Profession> GetProfessions(string debut, string maxRows)
        {
            _db = new InterBookEntities();
            var rp = (from m in _db.Ref_Profession
                     where m.Description.Contains(debut)
                      select m).Take(int.Parse(maxRows));
            List<Ref_Profession> lrp = null;
            if (rp != null)
            {
                lrp = new List<Ref_Profession>();
                foreach (Ref_Profession rpTemp in rp)
                {
                    lrp.Add(rpTemp);
                }
            }
            return lrp;
        }

        [WebMethod]
        public List<Ref_Ville> GetVille()
        {
            _db = new InterBookEntities();
            var rv = from m in _db.Ref_Ville
                     select m;
            List<Ref_Ville> lrv = null;
            if (rv != null)
            {
                lrv = new List<Ref_Ville>();
                foreach (Ref_Ville rvTemp in rv)
                {
                    lrv.Add(rvTemp);
                }
            }
            return lrv;
        }

        [WebMethod]
        public Util_Dispo GetUtilDispoByDateIdu(DateTime dt, int idu)
        {
            _db = new InterBookEntities();
            _db.Configuration.ProxyCreationEnabled = false;
            var ud = (from d in _db.Util_Dispo
                      join u in _db.Utils on d.idU equals u.IdU
                      where u.IdU == idu && d.dDispo == dt
                      select d).FirstOrDefault();

            if (ud != null)
                return ud;
            return null;
        }

        [WebMethod]
        public List<Util_Dispo> GetUtilDispoByIdu(int idu)
        {
            _db = new InterBookEntities();
            _db.Configuration.ProxyCreationEnabled = false;
            var uds = _db.GetUtilDispoByIdrdIdu(idu);
            List<Util_Dispo> lud = null;
            if (uds != null)
            {
                lud = new List<Util_Dispo>();
                foreach (Util_Dispo ud in uds)
                {
                    lud.Add(ud);
                }
            }
            return lud;
        }

        [WebMethod]
        public List<Util_Consentement> GetUtilConsentementByIdu(int idu)
        {
            _db = new InterBookEntities();
            _db.Configuration.ProxyCreationEnabled = false;

            var ucs = from m in _db.Util_Consentement where m.idu == idu select m;
            
            List<Util_Consentement> luc = null;
            if (ucs != null)
            {
                luc = new List<Util_Consentement>();
                foreach (Util_Consentement ud in ucs)
                {
                    luc.Add(ud);
                }
            }
            return luc;
        }

        [WebMethod]
        public DateTime ReturnSQLDate()
        {
            _db = new InterBookEntities();
            return new DateTime();
        }

        [WebMethod]
        public Guid ReturnSQLGuid()
        {
            _db = new InterBookEntities();
            Guid retour = Guid.NewGuid();

            return retour;
        }

        [WebMethod]
        public Util_Consentement GetUtilConsentementByIduIdm(int idU, int id_TypeConsentement)
        {
            _db = new InterBookEntities();
            var u = (from m in _db.Util_Consentement where m.idu == idU && m.id_TypeConsentement == id_TypeConsentement select m).FirstOrDefault();
            return u;
        }
    }
}
