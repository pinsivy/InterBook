using InterBook2._0.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (_db == null)
                _db = new InterBookEntities();

            var u = (from m in _db.Utils where m.IdU == idu select m).FirstOrDefault();
            return u;
        }

        [WebMethod]
        public Util GetUtilByUid(Guid? uid)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var u = (from m in _db.Utils where m.uid == uid select m).FirstOrDefault();
            return u;
        }

        [WebMethod]
        public List<Util> GetUtilsByVilleProfession(string Ville, string Profession)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var lu = _db.SearchByVilleProfession(Ville, Profession).ToList<Util>();
            return lu;
        }


        [WebMethod]
        public String GetUtilsByVilleProfessionExperienceContrat(string Ville, string Profession, string Experience, string Contrat)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var lu = _db.SearchByVilleProfessionExperienceContrat(Ville, Profession, Experience, Contrat).ToList<Util>();

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
        public void InsertLine(Util_Info ui)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(ui).State = ui.id_Util_Info == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Info_Entreprise uie)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(uie).State = uie.id_Util_Info_Entreprise == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Geo ug)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(ug).State = ug.id_Util_Geo == 0 ?
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
        public void InsertLine(Util_Message um)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(um).State = um.id_Util_Message == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Contact uc)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(uc).State = uc.id_Util_Contact == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Profession up)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(up).State = up.id_Util_Profession == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public void InsertLine(Reservation r)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(r).State = r.id_Reservation == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public void InsertLine(UE_envoi uee)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(uee).State = uee.id_UE_envoi == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public void InsertLine(Util_Favoris uf)
        {
            if (_db == null)
                _db = new InterBookEntities();

            _db.Entry(uf).State = uf.id_Util_Favoris == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            Save();
        }

        [WebMethod]
        public Util_Email ReturnUtilEmailByEmail(string email)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var ue = _db.SetUtilEmail(email);

            if (ue != null)
            {
                return ue.FirstOrDefault();
            }
            return null;
        }
        
        [WebMethod]
        public Util_Email GetUtilEmailByEmail(string email, bool particulier)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var ue = (from m in _db.Utils
                     join e in _db.Util_Email on m.idu_Email equals e.idu_Email
                      where e.email == email && m.particulier == particulier
                     select e).FirstOrDefault();

            if (ue != null)
                return ue;
            return null;
        }

        [WebMethod]
        public Util GetUtilByEmailMdp(bool particulier, string email, string mdp)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var u = (from m in _db.Utils
                     join e in _db.Util_Email on m.idu_Email equals e.idu_Email
                     where m.particulier == particulier && m.mdp == mdp && e.email == email
                     select m).FirstOrDefault();
            if (u != null)
                return u;
            return null;
        }

        [WebMethod]
        public Util GetUtilByEmail(string email)
        {
            if (_db == null)
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
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_Profession> lrp = (from m in _db.Ref_Profession
                     where m.Description.Contains(debut)
                      select m).Take(int.Parse(maxRows)).ToList<Ref_Profession>();
            return lrp;
        }

        [WebMethod]
        public List<Ref_VilleSimple> GetVilles(string debut, string maxRows)
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_VilleSimple> lrv = (from m in _db.Ref_Ville
                    where m.Description.Contains(debut)
                        || (m.article + m.ville).Contains(debut)
                        || m.cp.Contains(debut)
                    select new Ref_VilleSimple
                    {
                        id_Ville = m.id_Ville,
                        Description = m.Description,
                        cp = m.cp,
                        insee = m.insee,
                        article = m.article,
                        ville = m.ville,
                        id_Region = m.id_Region,
                        id_Departement = m.id_Departement,
                        longitude = m.longitude,
                        latitude  = m.latitude

                    }).Take(int.Parse(maxRows)).ToList<Ref_VilleSimple>();

            return lrv;
        }

        [WebMethod]
        public List<Ref_Region> GetRegions()
        {
            if (_db == null)
                _db = new InterBookEntities();
            //_db.Configuration.ProxyCreationEnabled = false;
            var rv = (from m in _db.Ref_Region
                      select m);
            List<Ref_Region> lrv = null;
            if (rv != null)
            {
                lrv = new List<Ref_Region>();
                foreach (Ref_Region rpTemp in rv)
                {
                    lrv.Add(rpTemp);
                }
            }
            return lrv;
        }

        [WebMethod]
        public List<Ref_Departement> GetDepartements()
        {
            if (_db == null)
                _db = new InterBookEntities();
            var lrv = (from m in _db.Ref_Departement
                       join n in _db.Ref_Region on m.id_Region equals n.id_Region
                       orderby n.Description, m.Description
                       select m).ToList<Ref_Departement>();
            return lrv;
        }

        [WebMethod]
        public Ref_Ville GetVilleByNom(string nom)
        {
            if (_db == null)
                _db = new InterBookEntities();
            //nom = Regex.Replace(nom, "[^a-zA-Z0-9_]", "").ToUpper();
            //_db.Configuration.ProxyCreationEnabled = false;
            var rv = (from m in _db.Ref_Ville
                      where m.Description == nom
                        || m.ville == nom
                        || (m.article + m.ville) == nom
                      select m).FirstOrDefault();
            if (rv == null)
                return null;
            return rv;
        }

        [WebMethod]
        public Ref_Ville GetVille(int idv)
        {
            if (_db == null)
                _db = new InterBookEntities();
            var rv = (from m in _db.Ref_Ville
                     where m.id_Ville == idv
                     select m).FirstOrDefault();
            if (rv == null)
                return null;
            return rv;
        }

        [WebMethod]
        public List<Util> GetUtilSearchByLongLatIddepRayonDate(double longitude, double latitude, string idD, int distance, DateTime? dDebut, DateTime? dFin)
        {
            if (_db == null)
                _db = new InterBookEntities();
            List<Util> lrv = _db.GetUtilSearchByLongLatIddepRayonDate(longitude, latitude, idD, distance, dDebut, dFin).ToList<Util>();

            return lrv;
        }

        [WebMethod]
        public List<Util> GetUtilSearchByLongLatIddepRayonDateAndParam(double longitude, double latitude, string idD, int distance, DateTime? dDebut, DateTime? dFin, string experience, string contrat)
        {
            if (_db == null)
                _db = new InterBookEntities();
            List<Util> lrv = _db.GetUtilSearchByLongLatIddepRayonDateAndParam(longitude, latitude, idD, distance, dDebut, dFin, experience, contrat).ToList<Util>();

            return lrv;
        }

        [WebMethod]
        public Ref_Ville GetVilleGeoloc(double longitude, double latitude, int distance)
        {
            if (_db == null)
                _db = new InterBookEntities();
            Ref_Ville rv = _db.GetRefVilleByLongLatRayon(longitude, latitude, distance).FirstOrDefault();

            return rv;
        }

        [WebMethod]
        public Util_Dispo GetUtilDispoByDateIdu(DateTime dt, int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Dispo ud = (from d in _db.Util_Dispo
                                    join u in _db.Utils on d.idU equals u.IdU
                                    where u.IdU == idu && d.dDispo == dt
                                    select d).FirstOrDefault();

            return ud;
        }

        [WebMethod]
        public List<Util_Dispo> GetUtilDispoByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            return _db.GetUtilDispoByIdrdIdu(idu).ToList<Util_Dispo>();
        }

        [WebMethod]
        public List<Util_Geo> GetUtilGeoByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Util_Geo> lum = (from m in _db.Util_Geo
                                  where m.idU == idu
                                    select m).ToList<Util_Geo>();

            return lum;
        }

        [WebMethod]
        public List<Util_Consentement> GetUtilConsentementByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();
            //_db.Configuration.ProxyCreationEnabled = false;

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
            if (_db == null)
                _db = new InterBookEntities();
            return new DateTime();
        }

        [WebMethod]
        public Guid ReturnSQLGuid()
        {
            if (_db == null)
                _db = new InterBookEntities();
            Guid retour = Guid.NewGuid();

            return retour;
        }

        [WebMethod]
        public Util_Consentement GetUtilConsentementByIduIdm(int idU, int id_TypeConsentement)
        {
            if (_db == null)
                _db = new InterBookEntities();
            var u = (from m in _db.Util_Consentement where m.idu == idU && m.id_TypeConsentement == id_TypeConsentement select m).FirstOrDefault();
            return u;
        }

        [WebMethod]
        public List<Ref_Experience> GetExperiences()
        {
            if (_db == null)
                _db = new InterBookEntities();
            var re = (from m in _db.Ref_Experience
                      select m);

            List<Ref_Experience> lre = null;
            if (re != null)
            {
                lre = new List<Ref_Experience>();
                foreach (Ref_Experience reTemp in re)
                {
                    lre.Add(reTemp);
                }
            }
            return lre;
        }

        [WebMethod]
        public List<Util_Message> GetUtilMessageByIdutoIdufrom(int iduFrom, int iduTo)
        {
            if (_db == null)
                _db = new InterBookEntities();
            //_db.Configuration.ProxyCreationEnabled = false;

            List<Util_Message> lum = (from m in _db.Util_Message
                      where (m.idUFrom == iduFrom && m.idUTo == iduTo)
                      || (m.idUFrom == iduTo && m.idUTo == iduFrom)
                      select m).OrderBy(x => x.dMessage).ToList<Util_Message>();
            return lum;
        }

        [WebMethod]
        public List<Util_Contact> GetUtilContactByIdufrom(int iduFrom)
        {
            if (_db == null)
                _db = new InterBookEntities();
            List<Util_Contact> luc = (from m in _db.Util_Contact
                      where (m.iduFrom == iduFrom)
                      select m).ToList<Util_Contact>();
            return luc;
        }

        [WebMethod]
        public Ref_Profession GetRefProfessionByDescription(string desc)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Ref_Profession rp = (from d in _db.Ref_Profession
                             where d.Description == desc
                             select d).FirstOrDefault();

            return rp;
        }

        [WebMethod]
        public Util_Favoris GetUtilFavorisByIduIduEnt(int idu, int iduEntr)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Favoris uf = (from d in _db.Util_Favoris
                                 where d.idU == idu && d.idUEntreprise == iduEntr
                                 select d).FirstOrDefault();

            return uf;
        }

        [WebMethod]
        public List<Ref_From> GetRefFromByIdfrom(int idfrom)
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_From> lrf = (from d in _db.Ref_From
                               where d.id_From == idfrom
                               select d).ToList<Ref_From>();

            return lrf;
        }

        [WebMethod]
        public List<Ref_Declinaison_Culture> SelectAllRefDeclinaisonCulture()
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_Declinaison_Culture> lrf = (from d in _db.Ref_Declinaison_Culture
                                                 select d).ToList<Ref_Declinaison_Culture>();

            return lrf;
        }

        [WebMethod]
        public List<Ref_Culture> SelectAllRefCulture()
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_Culture> lrf = (from d in _db.Ref_Culture
                                     select d).ToList<Ref_Culture>();

            return lrf;
        }

        [WebMethod]
        public List<Ref_Domaine> SelectAllRefDomaine()
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_Domaine> lrf = (from d in _db.Ref_Domaine
                                     select d).ToList<Ref_Domaine>();

            return lrf;
        }

        [WebMethod]
        public Reservation GetReservationByIdr(int idr)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Reservation r = (from d in _db.Reservations
                               where d.id_Reservation == idr
                               select d).FirstOrDefault();

            return r;
        }
    }
}
