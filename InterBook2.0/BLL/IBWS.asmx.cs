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
        public UtilSimple GetUtilByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var u = (from m in _db.Utils
                     where m.IdU == idu
                     select new UtilSimple
                     {
                        IdU = m.IdU,
                        id_Declinaison_Culture = m.id_Declinaison_Culture,
                        id_From = m.id_From,
                        mdp = m.mdp,
                        uid = m.uid,
                        idu_Email = m.idu_Email,
                        idu_Postal = m.idu_Postal,
                        idu_Telmobile = m.idu_Telmobile,
                        dCrea = m.dCrea,
                        dMAJ = m.dMAJ,
                        particulier = m.particulier,
                        id_Util_Info = m.id_Util_Info,
                        id_Util_Info_Entreprise = m.id_Util_Info_Entreprise
                     }).FirstOrDefault();

            return u;
        }

        [WebMethod]
        public UtilSimple GetUtilByUid(Guid? uid)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var u = (from m in _db.Utils
                     where m.uid == uid
                     select new UtilSimple
                     {
                         IdU = m.IdU,
                         id_Declinaison_Culture = m.id_Declinaison_Culture,
                         id_From = m.id_From,
                         mdp = m.mdp,
                         uid = m.uid,
                         idu_Email = m.idu_Email,
                         idu_Postal = m.idu_Postal,
                         idu_Telmobile = m.idu_Telmobile,
                         dCrea = m.dCrea,
                         dMAJ = m.dMAJ,
                         particulier = m.particulier,
                         id_Util_Info = m.id_Util_Info,
                         id_Util_Info_Entreprise = m.id_Util_Info_Entreprise
                     }).FirstOrDefault();
            return u;
        }



        [WebMethod]
        public void InsertLine_Util(UtilSimple us)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util u = new Util
            {
                IdU = us.IdU,
                id_Declinaison_Culture = us.id_Declinaison_Culture,
                id_From = us.id_From,
                mdp = us.mdp,
                uid = us.uid,
                idu_Email = us.idu_Email,
                idu_Postal = us.idu_Postal,
                idu_Telmobile = us.idu_Telmobile,
                dCrea = us.dCrea,
                dMAJ = us.dMAJ,
                particulier = us.particulier,
                id_Util_Info = us.id_Util_Info,
                id_Util_Info_Entreprise = us.id_Util_Info_Entreprise
            };

            var entry = _db.Entry<Util>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Dispo>();
                Util_Dispo attachedEntity = set.Local.SingleOrDefault(e => e.idU == u.IdU);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.IdU == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Info(Util_InfoSimple ui)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Info u = new Util_Info
            {
                id_Util_Info = ui.id_Util_Info,
                photo = ui.photo,
                cv = ui.cv,
                numTel = ui.numTel,
                dNaissance = ui.dNaissance,
                experience = ui.experience,
                motivation = ui.motivation,
                permisA = ui.permisA,
                permisB = ui.permisB,
                permisC = ui.permisC
            };

            var entry = _db.Entry<Util_Info>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Info>();
                Util_Info attachedEntity = set.Local.SingleOrDefault(e => e.id_Util_Info == u.id_Util_Info);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_Util_Info == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Info_Entreprise(Util_Info_EntrepriseSimple uie)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Info_Entreprise u = new Util_Info_Entreprise
            {
                id_Util_Info_Entreprise = uie.id_Util_Info_Entreprise,
                Nom = uie.Nom,
                Email = uie.Email,
                Ville = uie.Ville,
                Tel = uie.Tel,
                Fax = uie.Fax,
                APE = uie.APE,
                Logo = uie.Logo,
                Siret = uie.Siret,
                Siren = uie.Siren,
                SiteWeb = uie.SiteWeb
            };

            var entry = _db.Entry<Util_Info_Entreprise>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Info_Entreprise>();
                Util_Info_Entreprise attachedEntity = set.Local.SingleOrDefault(e => e.id_Util_Info_Entreprise == u.id_Util_Info_Entreprise);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_Util_Info_Entreprise == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Geo(Util_GeoSimple ug)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Geo u = new Util_Geo
            {
                id_Util_Geo = ug.id_Util_Geo,
                id_Departement = ug.id_Departement,
                id_Region = ug.id_Region,
                idU = ug.idU
            };

            var entry = _db.Entry<Util_Geo>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Geo>();
                Util_Geo attachedEntity = set.Local.SingleOrDefault(e => e.id_Util_Geo == u.id_Util_Geo);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_Util_Geo == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Postal(Util_PostalSimple up)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Postal u = new Util_Postal
            {
                idu_Postal = up.idu_Postal,
                id_Civilite = up.id_Civilite,
                nom = up.nom,
                prenom = up.prenom,
                adresse1 = up.adresse1,
                adresse2 = up.adresse2,
                cp = up.cp,
                id_Ville = up.id_Ville,
                id_Pays = up.id_Pays,
                dCrea = up.dCrea
            };

            var entry = _db.Entry<Util_Postal>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Postal>();
                Util_Postal attachedEntity = set.Local.SingleOrDefault(e => e.idu_Postal == u.idu_Postal);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.idu_Postal == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Email(Util_EmailSimple ue)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Email u = new Util_Email
            {
                idu_Email = ue.idu_Email,
                email = ue.email,
                dCrea = ue.dCrea
            };

            var entry = _db.Entry<Util_Email>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Email>();
                Util_Email attachedEntity = set.Local.SingleOrDefault(e => e.idu_Email == u.idu_Email);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.idu_Email == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Dispo(Util_DispoSimple uds)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Dispo ud = new Util_Dispo
            {
                id_Util_Dispo = uds.id_Util_Dispo,
                dDispo = uds.dDispo,
                idU = uds.idU,
                id_Ref_Dispo = uds.id_Ref_Dispo
            };

            var entry = _db.Entry<Util_Dispo>(ud);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Dispo>();
                Util_Dispo attachedEntity = set.Local.SingleOrDefault(e => e.id_Util_Dispo == ud.id_Util_Dispo);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(ud);
                }
                else
                {
                    entry.State = ud.id_Util_Dispo == 0 ? EntityState.Added : EntityState.Modified;
                }
            }

            //_db.Entry(ud).State = ud.id_Util_Dispo == 0 ?
            //                       EntityState.Added :
            //                       EntityState.Modified;

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Consentement(Util_ConsentementSimple uc)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Consentement u = new Util_Consentement
            {
                id_Util_Consentement = uc.id_Util_Consentement,
                id_TypeConsentement = uc.id_TypeConsentement,
                valeur = uc.valeur,
                id_Mailing = uc.id_Mailing,
                idu = uc.idu,
                dRecueil = uc.dRecueil
            };

            var entry = _db.Entry<Util_Consentement>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Consentement>();
                Util_Consentement attachedEntity = set.Local.SingleOrDefault(e => e.id_Util_Consentement == u.id_Util_Consentement);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_Util_Consentement == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Message(Util_MessageSimple um)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Message u = new Util_Message
            {
                id_Util_Message = um.id_Util_Message,
                dMessage = um.dMessage,
                idUFrom = um.idUFrom,
                idUTo = um.idUTo,
                message = um.message
            };

            var entry = _db.Entry<Util_Message>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Message>();
                Util_Message attachedEntity = set.Local.SingleOrDefault(e => e.id_Util_Message == u.id_Util_Message);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_Util_Message == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Contact(string id_Util_Contact, string iduFrom, string iduTo)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Contact u = new Util_Contact
            {
                id_Util_Contact = int.Parse(id_Util_Contact),
                iduFrom = int.Parse(iduFrom),
                iduTo = int.Parse(iduTo)
            };

            var entry = _db.Entry<Util_Contact>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Contact>();
                Util_Contact attachedEntity = set.Local.SingleOrDefault(e => e.id_Util_Contact == u.id_Util_Contact);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_Util_Contact == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Profession(Util_ProfessionSimple up)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Profession u = new Util_Profession
            {
                id_Util_Profession = up.id_Util_Profession,
                id_Profession = up.id_Profession,
                IdU = up.IdU
            };

            var entry = _db.Entry<Util_Profession>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Profession>();
                Util_Profession attachedEntity = set.Local.SingleOrDefault(e => e.id_Util_Profession == u.id_Util_Profession);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_Util_Profession == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        
        [WebMethod]
        public void InsertLine_Reservation(string id_Reservation, string idUEntreprise, string debut, string fin, string id_EtatReservation, string idUEmploye)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Reservation u = new Reservation
            {
                id_Reservation = int.Parse(id_Reservation),
                idUEntreprise = int.Parse(idUEntreprise),
                debut = DateTime.Parse(debut),
                fin = DateTime.Parse(fin),
                id_EtatReservation = int.Parse(id_EtatReservation),
                idUEmploye = int.Parse(idUEmploye)
            };

            var entry = _db.Entry<Reservation>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Reservation>();
                Reservation attachedEntity = set.Local.SingleOrDefault(e => e.id_Reservation == u.id_Reservation);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_Reservation == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void deleteLine_Reservation(string id_Reservation)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Reservation u = new Reservation
            {
                id_Reservation = int.Parse(id_Reservation)
            };

            var entry = _db.Entry<Reservation>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Reservation>();
                Reservation attachedEntity = set.Local.SingleOrDefault(e => e.id_Reservation == u.id_Reservation);  // You need to have access to key
                entry.State = EntityState.Deleted;
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_UE_envoi(UE_envoiSimple uee)
        {
            if (_db == null)
                _db = new InterBookEntities();

            UE_envoi u = new UE_envoi
            {
                id_UE_envoi = uee.id_UE_envoi,
                id_mailing = uee.id_mailing,
                idu_email = uee.idu_email,
                idu = uee.idu,
                dEnvoi = uee.dEnvoi
            };

            var entry = _db.Entry<UE_envoi>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<UE_envoi>();
                UE_envoi attachedEntity = set.Local.SingleOrDefault(e => e.id_UE_envoi == u.id_UE_envoi);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_UE_envoi == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Favoris(Util_FavorisSimple uf)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Favoris u = new Util_Favoris
            {
                id_Util_Favoris = uf.id_Util_Favoris,
                idU = uf.idU,
                idUEntreprise = uf.idUEntreprise,
                actif = uf.actif
            };

            var entry = _db.Entry<Util_Favoris>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Favoris>();
                Util_Favoris attachedEntity = set.Local.SingleOrDefault(e => e.id_Util_Favoris == u.id_Util_Favoris);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_Util_Favoris == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public void InsertLine_Util_Android(string id_Util_Android, string registerid, string idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Android u = new Util_Android
            {
                id_Util_Android = int.Parse(id_Util_Android),
                registerid = registerid,
                idu = int.Parse(idu),
            };

            var entry = _db.Entry<Util_Android>(u);

            if (entry.State == EntityState.Detached)
            {
                var set = _db.Set<Util_Android>();
                Util_Android attachedEntity = set.Local.SingleOrDefault(e => e.id_Util_Android == u.id_Util_Android);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(u);
                }
                else
                {
                    entry.State = u.id_Util_Android == 0 ? EntityState.Added : EntityState.Modified; // This should attach entity
                }
            }

            Save();
        }

        [WebMethod]
        public Util_EmailSimple ReturnUtilEmailByEmail(string email)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var ue = _db.SetUtilEmail(email);

            if (ue != null)
            {
                Util_Email ueTemp = ue.FirstOrDefault();
                Util_EmailSimple ues = new Util_EmailSimple {
                    idu_Email = ueTemp.idu_Email,
                    email = ueTemp.email,
                    dCrea = ueTemp.dCrea
                };
                return ues;
            }
            return null;
        }
        
        [WebMethod]
        public Util_EmailSimple GetUtilEmailByEmail(string email, bool particulier)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var ue = (from m in _db.Utils
                     join e in _db.Util_Email on m.idu_Email equals e.idu_Email
                      where e.email == email && m.particulier == particulier
                      select new Util_EmailSimple
                      {
                          idu_Email = e.idu_Email,
                          email = e.email,
                          dCrea = e.dCrea

                      }).FirstOrDefault();

            if (ue != null)
                return ue;
            return null;
        }

        [WebMethod]
        public Util_EmailSimple GetUtilEmailByIduEmail(int idue)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var ue = (from m in _db.Utils
                      join e in _db.Util_Email on m.idu_Email equals e.idu_Email
                      where e.idu_Email == idue
                      select new Util_EmailSimple
                      {
                          idu_Email = e.idu_Email,
                          email = e.email,
                          dCrea = e.dCrea

                      }).FirstOrDefault();

            return ue;
        }

        [WebMethod]
        public UtilSimple GetUtilByEmailMdp(bool particulier, string email, string mdp)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var u = (from m in _db.Utils
                     join e in _db.Util_Email on m.idu_Email equals e.idu_Email
                     where m.particulier == particulier && m.mdp == mdp && e.email == email
                     select new UtilSimple
                     {
                         IdU = m.IdU,
                         id_Declinaison_Culture = m.id_Declinaison_Culture,
                         id_From = m.id_From,
                         mdp = m.mdp,
                         uid = m.uid,
                         idu_Email = m.idu_Email,
                         idu_Postal = m.idu_Postal,
                         idu_Telmobile = m.idu_Telmobile,
                         dCrea = m.dCrea,
                         dMAJ = m.dMAJ,
                         particulier = m.particulier,
                         id_Util_Info = m.id_Util_Info,
                         id_Util_Info_Entreprise = m.id_Util_Info_Entreprise
                     }).FirstOrDefault();
            if (u != null)
                return u;
            return null;
        }

        [WebMethod]
        public UtilSimple GetUtilByEmail(string email)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var u = (from m in _db.Utils
                     join e in _db.Util_Email on m.idu_Email equals e.idu_Email
                     where e.email == email
                     select new UtilSimple
                     {
                         IdU = m.IdU,
                         id_Declinaison_Culture = m.id_Declinaison_Culture,
                         id_From = m.id_From,
                         mdp = m.mdp,
                         uid = m.uid,
                         idu_Email = m.idu_Email,
                         idu_Postal = m.idu_Postal,
                         idu_Telmobile = m.idu_Telmobile,
                         dCrea = m.dCrea,
                         dMAJ = m.dMAJ,
                         particulier = m.particulier,
                         id_Util_Info = m.id_Util_Info,
                         id_Util_Info_Entreprise = m.id_Util_Info_Entreprise
                     }).FirstOrDefault();
            if (u != null)
                return u;
            return null;
        }

        [WebMethod]
        public List<Ref_ProfessionSimple> GetProfessions(string debut, string maxRows)
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_ProfessionSimple> lrp = (from m in _db.Ref_Profession
                     where m.Description.Contains(debut)
                        select new Ref_ProfessionSimple
                        {
                            id_Profession = m.id_Profession,
                            Description = m.Description

                        }).Take(int.Parse(maxRows)).ToList<Ref_ProfessionSimple>();
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
        public List<Ref_RegionSimple> GetRegions()
        {
            if (_db == null)
                _db = new InterBookEntities();
            //_db.Configuration.ProxyCreationEnabled = false;
            var lrv = (from m in _db.Ref_Region
                      select new Ref_RegionSimple
                      {
                          id_Region = m.id_Region,
                          Description = m.Description

                      }).ToList<Ref_RegionSimple>();
            return lrv;
        }

        [WebMethod]
        public List<Ref_DepartementSimple> GetDepartements()
        {
            if (_db == null)
                _db = new InterBookEntities();
            var lrv = (from m in _db.Ref_Departement
                       join n in _db.Ref_Region on m.id_Region equals n.id_Region
                       orderby n.Description, m.Description
                       select new Ref_DepartementSimple
                       {
                           id_Departement = m.id_Departement,
                           Description = m.Description

                       }).ToList<Ref_DepartementSimple>();
            return lrv;
        }

        [WebMethod]
        public Ref_VilleSimple GetVilleByNom(string nom)
        {
            if (_db == null)
                _db = new InterBookEntities();
            //nom = Regex.Replace(nom, "[^a-zA-Z0-9_]", "").ToUpper();
            //_db.Configuration.ProxyCreationEnabled = false;
            var rv = (from m in _db.Ref_Ville
                      where m.Description == nom
                        || m.ville == nom
                        || (m.article + m.ville) == nom
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
                          latitude = m.latitude

                      }).FirstOrDefault();

            return rv;
        }

        [WebMethod]
        public Ref_VilleSimple GetRefVilleByIduPostal(int idup)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var rv = (from m in _db.Ref_Ville
                      join u in _db.Util_Postal on m.id_Ville equals u.id_Ville
                      where u.idu_Postal == idup
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
                          latitude = m.latitude

                      }).FirstOrDefault();

            return rv;
        }

        [WebMethod]
        public List<UtilSimple> GetUtilSearchByLongLatIddepRayonDate(double longitude, double latitude, string idD, int distance, DateTime? dDebut, DateTime? dFin)
        {
            if (_db == null)
                _db = new InterBookEntities();
            List<Util> lrv = _db.GetUtilSearchByLongLatIddepRayonDate(longitude, latitude, idD, distance, dDebut, dFin).ToList<Util>();
            List<UtilSimple> lrs = new List<UtilSimple>();
            foreach(Util uTemp in lrv)
            {
                UtilSimple usTemp = new UtilSimple
                {
                    IdU = uTemp.IdU,
                    id_Declinaison_Culture = uTemp.id_Declinaison_Culture,
                    id_From = uTemp.id_From,
                    mdp = uTemp.mdp,
                    uid = uTemp.uid,
                    idu_Email = uTemp.idu_Email,
                    idu_Postal = uTemp.idu_Postal,
                    idu_Telmobile = uTemp.idu_Telmobile,
                    dCrea = uTemp.dCrea,
                    dMAJ = uTemp.dMAJ,
                    particulier = uTemp.particulier,
                    id_Util_Info = uTemp.id_Util_Info,
                    id_Util_Info_Entreprise = uTemp.id_Util_Info_Entreprise
                };
                lrs.Add(usTemp);
            }

            return lrs;
        }

        [WebMethod]
        public List<UtilSimple> GetUtilSearchByLongLatIddepRayonDateAndParam(double longitude, double latitude, string idD, int distance, DateTime? dDebut, DateTime? dFin, string experience, string contrat)
        {
            if (_db == null)
                _db = new InterBookEntities();
            List<Util> lrv = _db.GetUtilSearchByLongLatIddepRayonDateAndParam(longitude, latitude, idD, distance, dDebut, dFin, experience, contrat).ToList<Util>();
            List<UtilSimple> lrs = new List<UtilSimple>();
            foreach (Util uTemp in lrv)
            {
                UtilSimple usTemp = new UtilSimple
                {
                    IdU = uTemp.IdU,
                    id_Declinaison_Culture = uTemp.id_Declinaison_Culture,
                    id_From = uTemp.id_From,
                    mdp = uTemp.mdp,
                    uid = uTemp.uid,
                    idu_Email = uTemp.idu_Email,
                    idu_Postal = uTemp.idu_Postal,
                    idu_Telmobile = uTemp.idu_Telmobile,
                    dCrea = uTemp.dCrea,
                    dMAJ = uTemp.dMAJ,
                    particulier = uTemp.particulier,
                    id_Util_Info = uTemp.id_Util_Info,
                    id_Util_Info_Entreprise = uTemp.id_Util_Info_Entreprise
                };
                lrs.Add(usTemp);
            }
            return lrs;
        }

        [WebMethod]
        public Ref_VilleSimple GetVilleGeoloc(double longitude, double latitude, int distance)
        {
            if (_db == null)
                _db = new InterBookEntities();
            Ref_Ville rv = _db.GetRefVilleByLongLatRayon(longitude, latitude, distance).FirstOrDefault();
            Ref_VilleSimple rvs = new Ref_VilleSimple
            {
                id_Ville = rv.id_Ville,
                Description = rv.Description,
                cp = rv.cp,
                insee = rv.insee,
                article = rv.article,
                ville = rv.ville,
                id_Region = rv.id_Region,
                id_Departement = rv.id_Departement,
                longitude = rv.longitude,
                latitude = rv.latitude

            };
            return rvs;
        }

        [WebMethod]
        public Util_DispoSimple GetUtilDispoByDateIdu(DateTime dt, int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var ud = (from d in _db.Util_Dispo
                                    join u in _db.Utils on d.idU equals u.IdU
                                    where u.IdU == idu && d.dDispo == dt
                                    select new Util_DispoSimple
                                    {
                                        id_Util_Dispo = d.id_Util_Dispo,
                                        dDispo = d.dDispo,
                                        idU = d.idU,
                                        id_Ref_Dispo = d.id_Ref_Dispo
                                    }).FirstOrDefault();

            return ud;
        }

        [WebMethod]
        public List<Util_DispoSimple> GetUtilDispoByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Util_Dispo> lud = _db.GetUtilDispoByIdrdIdu(idu).ToList<Util_Dispo>();
            List<Util_DispoSimple> luds = new List<Util_DispoSimple>();
            foreach (Util_Dispo udTemp in lud)
            {
                Util_DispoSimple udsTemp = new Util_DispoSimple
                {
                    id_Util_Dispo = udTemp.id_Util_Dispo,
                    dDispo = udTemp.dDispo,
                    idU = udTemp.idU,
                    id_Ref_Dispo = udTemp.id_Ref_Dispo
                };
                luds.Add(udsTemp);
            }
            return luds;
        }

        [WebMethod]
        public List<Util_GeoSimple> GetUtilGeoByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Util_GeoSimple> lum = (from m in _db.Util_Geo
                                    where m.idU == idu
                                    select new Util_GeoSimple
                                    {
                                        id_Util_Geo = m.id_Util_Geo,
                                        id_Departement = m.id_Departement,
                                        id_Region = m.id_Region,
                                        idU = m.idU
                                    }).ToList<Util_GeoSimple>();

            return lum;
        }

        [WebMethod]
        public List<Util_ConsentementSimple> GetUtilConsentementByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();
            //_db.Configuration.ProxyCreationEnabled = false;

            var lucs = (from m in _db.Util_Consentement
                       where m.idu == idu
                       select new Util_ConsentementSimple
                       {
                           id_Util_Consentement = m.id_Util_Consentement,
                           id_TypeConsentement = m.id_TypeConsentement,
                           valeur = m.valeur,
                           id_Mailing = m.id_Mailing,
                           idu = m.idu,
                           dRecueil = m.dRecueil
                       }).ToList<Util_ConsentementSimple>();

            return lucs;
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
        public Util_ConsentementSimple GetUtilConsentementByIduIdm(int idU, int id_TypeConsentement)
        {
            if (_db == null)
                _db = new InterBookEntities();
            var u = (from m in _db.Util_Consentement
                     where m.idu == idU && m.id_TypeConsentement == id_TypeConsentement
                     select new Util_ConsentementSimple
                     {
                         id_Util_Consentement = m.id_Util_Consentement,
                         id_TypeConsentement = m.id_TypeConsentement,
                         valeur = m.valeur,
                         id_Mailing = m.id_Mailing,
                         idu = m.idu,
                         dRecueil = m.dRecueil
                     }).FirstOrDefault();
            return u;
        }

        [WebMethod]
        public List<Ref_ExperienceSimple> GetExperiences()
        {
            if (_db == null)
                _db = new InterBookEntities();
            var lre = (from m in _db.Ref_Experience
                      select new Ref_ExperienceSimple
                      {
                          id_Experience = m.id_Experience,
                          Description = m.Description
                      }).ToList<Ref_ExperienceSimple>();

            return lre;
        }

        [WebMethod]
        public Ref_ProfessionSimple GetRefProfessionByDescription(string desc)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Ref_ProfessionSimple rp = (from m in _db.Ref_Profession
                                    where m.Description == desc
                                    select new Ref_ProfessionSimple
                                    {
                                        id_Profession = m.id_Profession,
                                        Description = m.Description
                                    }).FirstOrDefault();

            return rp;
        }

        [WebMethod]
        public List<Util_MessageSimple> GetUtilMessageByIdutoIdufrom(int iduFrom, int iduTo)
        {
            if (_db == null)
                _db = new InterBookEntities();
            //_db.Configuration.ProxyCreationEnabled = false;

            List<Util_MessageSimple> lums = (from m in _db.Util_Message
                      where (m.idUFrom == iduFrom && m.idUTo == iduTo)
                      || (m.idUFrom == iduTo && m.idUTo == iduFrom)
                        select new Util_MessageSimple
                        {
                            id_Util_Message = m.id_Util_Message,
                            dMessage = m.dMessage,
                            idUFrom = m.idUFrom,
                            idUTo = m.idUTo,
                            message = m.message
                        }).OrderBy(x => x.dMessage).ToList<Util_MessageSimple>();
            return lums;
        }                   

        [WebMethod]
        public List<Util_ContactSimple> GetUtilContactByIdufrom(int iduFrom)
        {
            if (_db == null)
                _db = new InterBookEntities();
            List<Util_ContactSimple> luc = (from m in _db.Util_Contact
                      where (m.iduFrom == iduFrom)
                        select new Util_ContactSimple
                        {
                            id_Util_Contact = m.id_Util_Contact,
                            iduFrom = m.iduFrom,
                            iduTo = m.iduTo
                        }).ToList<Util_ContactSimple>();
            return luc;
        }

        [WebMethod]
        public Util_FavorisSimple GetUtilFavorisByIduIduEnt(int idu, int iduEntr)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_FavorisSimple uf = (from m in _db.Util_Favoris
                                 where m.idU == idu && m.idUEntreprise == iduEntr
                                    select new Util_FavorisSimple
                                    {
                                        id_Util_Favoris = m.id_Util_Favoris,
                                        idU = m.idU,
                                        idUEntreprise = m.idUEntreprise,
                                        actif = m.actif
                                    }).FirstOrDefault();

            return uf;
        }

        [WebMethod]
        public List<Ref_FromSimple> GetRefFromByIdfrom(int idfrom)
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_FromSimple> lrf = (from d in _db.Ref_From
                                    where d.id_From == idfrom
                                    select new Ref_FromSimple
                                    {
                                        id_From = d.id_From,
                                        Description = d.Description,
                                        id_Declinaison_Culture = d.id_Declinaison_Culture
                                    }).ToList<Ref_FromSimple>();

            return lrf;
        }

        [WebMethod]
        public List<Ref_Declinaison_CultureSimple> SelectAllRefDeclinaisonCulture()
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_Declinaison_CultureSimple> lrf = (from d in _db.Ref_Declinaison_Culture
                                                       select new Ref_Declinaison_CultureSimple
                                                       {
                                                           id_Declinaison_Culture = d.id_Declinaison_Culture,
                                                           id_Culture = d.id_Culture,
                                                           id_Declinaison = d.id_Declinaison,
                                                           id_From_Defaut = d.id_From_Defaut,
                                                           EmailFromSupport = d.EmailFromSupport
                                                       }).ToList<Ref_Declinaison_CultureSimple>();

            return lrf;
        }

        [WebMethod]
        public List<Ref_CultureSimple> SelectAllRefCulture()
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_CultureSimple> lrf = (from d in _db.Ref_Culture
                                           select new Ref_CultureSimple
                                           {
                                               id_Culture = d.id_Culture,
                                               description = d.description
                                           }).ToList<Ref_CultureSimple>();

            return lrf;
        }

        [WebMethod]
        public Ref_CultureSimple GetRefCultureByIdDeclCult(int iddc)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Ref_CultureSimple lrf = (from d in _db.Ref_Culture
                                           join u in _db.Ref_Declinaison_Culture on d.id_Culture equals u.id_Culture
                                           where u.id_Declinaison_Culture == iddc
                                           select new Ref_CultureSimple
                                           {
                                               id_Culture = d.id_Culture,
                                               description = d.description
                                           }).FirstOrDefault();

            return lrf;
        }

        [WebMethod]
        public List<Ref_DomaineSimple> SelectAllRefDomaine()
        {
            if (_db == null)
                _db = new InterBookEntities();

            List<Ref_DomaineSimple> lrf = (from d in _db.Ref_Domaine
                                           select new Ref_DomaineSimple
                                           {
                                               id_Domaine = d.id_Domaine,
                                               Domaine = d.Domaine,
                                               id_Declinaison_Culture = d.id_Declinaison_Culture
                                           }).ToList<Ref_DomaineSimple>();

            return lrf;
        }

        [WebMethod]
        public ReservationSimple GetReservationByIdr(int idr)
        {
            if (_db == null)
                _db = new InterBookEntities();

            ReservationSimple r = (from m in _db.Reservations
                               where m.id_Reservation == idr
                                select new ReservationSimple
                                {
                                    id_Reservation = m.id_Reservation,
                                    idUEntreprise = m.idUEntreprise,
                                    debut = m.debut,
                                    fin = m.fin,
                                    id_EtatReservation = m.id_EtatReservation,
                                    idUEmploye = m.idUEmploye
                                }).FirstOrDefault();

            return r;
        }

        [WebMethod]
        public Util_PostalSimple GetUtilPostalByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_PostalSimple r = (from m in _db.Util_Postal
                                   join u in _db.Utils on m.idu_Postal equals u.idu_Postal
                                   where u.IdU == idu
                                   select new Util_PostalSimple
                                   {
                                       idu_Postal = m.idu_Postal,
                                       id_Civilite = m.id_Civilite,
                                       nom = m.nom,
                                       prenom = m.prenom,
                                       adresse1 =  m.adresse1,
                                       adresse2 = m.adresse2,
                                       cp = m.cp,
                                       id_Ville = m.id_Ville,
                                       id_Pays = m.id_Pays,
                                       dCrea = m.dCrea
                                   }).FirstOrDefault();

            return r;
        }

        [WebMethod]
        public Util_InfoSimple GetUtilInfoByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_InfoSimple r = (from m in _db.Util_Info
                                            join u in _db.Utils on m.id_Util_Info equals u.id_Util_Info
                                            where u.IdU == idu
                                            select new Util_InfoSimple
                                            {
                                                id_Util_Info = m.id_Util_Info,
                                                photo = m.photo,
                                                cv = m.cv,
                                                numTel = m.numTel,
                                                dNaissance = m.dNaissance,
                                                experience = m.experience,
                                                motivation = m.motivation,
                                                permisA = m.permisA,
                                                permisB = m.permisB,
                                                permisC = m.permisC
                                            }).FirstOrDefault();

            return r;
        }

        [WebMethod]
        public Util_Info_EntrepriseSimple GetUtilInfoEntrepriseByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            Util_Info_EntrepriseSimple r = (from m in _db.Util_Info_Entreprise
                                   join u in _db.Utils on m.id_Util_Info_Entreprise equals u.id_Util_Info_Entreprise
                                   where u.IdU == idu
                                    select new Util_Info_EntrepriseSimple
                                   {
                                       id_Util_Info_Entreprise = m.id_Util_Info_Entreprise,
                                       Nom = m.Nom,
                                       Email = m.Email,
                                       Ville = m.Ville,
                                       Tel = m.Tel,
                                       Fax = m.Fax,
                                       APE = m.APE,
                                       Logo = m.Logo,
                                       Siret = m.Siret,
                                       Siren = m.Siren,
                                       SiteWeb = m.SiteWeb
                                   }).FirstOrDefault();

            return r;
        }

        [WebMethod]
        public Util_AndroidSimple GetUtilAndroidByRegidIdu(string registerid, int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var u = (from m in _db.Util_Android
                     join e in _db.Utils on m.idu equals e.IdU
                     where m.registerid == registerid && m.idu == idu
                     select new Util_AndroidSimple
                     {
                         id_Util_Android = m.id_Util_Android,
                         registerid = m.registerid,
                         idU = m.idu
                     }).FirstOrDefault();
            return u;
        }

        [WebMethod]
        public List<Util_AndroidSimple> GetUtilAndroidByIdu(int idu)
        {
            if (_db == null)
                _db = new InterBookEntities();

            var u = (from m in _db.Util_Android
                     join e in _db.Utils on m.idu equals e.IdU
                     where m.idu == idu
                     select new Util_AndroidSimple
                     {
                         id_Util_Android = m.id_Util_Android,
                         registerid = m.registerid,
                         idU = m.idu
                     }).ToList<Util_AndroidSimple>();
            return u;
        }
    }
}
