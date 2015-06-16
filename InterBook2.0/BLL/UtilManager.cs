using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    [Serializable]
    public class UtilManager
    {
        public static void InsertLine(UtilSimple us, bool storeInSession)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            //changer en utilSimple
            Util u = null;//SessionManager.Current.ws.GetUtilByIdu(idu);
            SessionManager.Current.ws.InsertLine_Util(us);

            if (storeInSession)
                SessionManager.Current.Util = u;
        }

        public static UtilSimple GetUtilByIdU(int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            UtilSimple us = SessionManager.Current.ws.GetUtilByIdu(idu);

            return us;
        }

        public static Util GetUtilByUid(Guid? uid)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            UtilSimple us = SessionManager.Current.ws.GetUtilByUid(uid);
            Util u = new Util()
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
            us = null;
            return u;
        }

        public static String GetIduByUtil(List<Util> u)
        {
            return u[0].IdU.ToString();
        }

        public static Util GetCompletUtilByIdu(int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            InterBookEntities _db = new InterBookEntities();

            var u = (from m in _db.Utils
                     where m.IdU == idu
                     select m).FirstOrDefault();

            return u;
        }

        public static Util GetCompletUtilByEmail(string email)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            InterBookEntities _db = new InterBookEntities();

            var u = (from m in _db.Utils
                     join e in _db.Util_Email on m.idu_Email equals e.idu_Email
                     where e.email == email
                     select m).FirstOrDefault();

            return u;
        }

        public static UtilSimple GetUtilByEmail(string email)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilByEmail(email);
        }

        public static UtilSimple GetUtilByEmailMdp(bool particulier, string email, string mdp)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilByEmailMdp(particulier, email, mdp);
        }

        public static List<Ref_ProfessionSimple> GetProfessions(string debut, string maxRows)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetProfessions(debut, maxRows);
        }

        public static List<Ref_VilleSimple> GetVilles(string debut, string maxRows)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return  SessionManager.Current.ws.GetVilles(debut, maxRows);
        }

        public static Ref_VilleSimple GetVilleGeoloc(double latitude, double longitude, int distance)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            
            Ref_VilleSimple rvs = SessionManager.Current.ws.GetVilleGeoloc(latitude, longitude, distance);

            return rvs;
        }

        public static List<Ref_RegionSimple> GetRegions()
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetRegions();
        }

        public static List<Ref_DepartementSimple> GetDepartements()
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetDepartements();
        }

        public static List<Ref_ExperienceSimple> GetExperiences()
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetExperiences();
        }

        public static List<Ref_Departement> GetCompletDepartements()
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            List<Ref_Departement> ld = new List<Ref_Departement>();

            return ld;
        }
    }
}