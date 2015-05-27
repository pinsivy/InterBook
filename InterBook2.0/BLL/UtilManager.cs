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
        public static void InsertLine(Util util, bool storeInSession)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine(util);

            if (storeInSession)
                SessionManager.Current.Util = util;
        }

        public static Util GetUtilByIdU(int idU)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilByIdu(idU);
        }

        public static Util GetUtilByUid(Guid? uid)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilByUid(uid);
        }

        public static String GetIduByUtil(List<Util> u)
        {
            return u[0].IdU.ToString();
        }

        public static Util GetUtilByEmail(string email)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilByEmail(email);
        }

        public static Util GetUtilByEmailMdp(bool particulier, string email, string mdp)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilByEmailMdp(particulier, email, mdp);
        }

        public static List<Ref_Profession> GetProfessions(string debut, string maxRows)
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
            
            Ref_Ville rv = SessionManager.Current.ws.GetVilleGeoloc(latitude, longitude, distance);

            Ref_VilleSimple rvs = new Ref_VilleSimple()
            {
                id_Ville = rv.id_Ville,
                Description	= rv.Description,
                cp = rv.cp,
                insee = rv.insee,
                article = rv.article,
                ville = rv.ville,
                id_Region = rv.id_Region,
                id_Departement = rv.id_Departement,
                longitude = rv.longitude,
                latitude = rv.latitude
            };

            rv = null;
            return rvs;
        }

        public static List<Ref_Region> GetRegions()
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetRegions();
        }

        public static List<Ref_Departement> GetDepartements()
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetDepartements();
        }

        public static List<Ref_Experience> GetExperiences()
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetExperiences();
        }
    }
}