using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilGeoManager
    {
        public static void InsertLine(Util_Geo UtilInfo)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine(UtilInfo);
        }

        public static List<Util_GeoSimple> GetUtilGeoByIdu(int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            List<Util_Geo> lud = SessionManager.Current.ws.GetUtilGeoByIdu(idu);
            List<Util_GeoSimple> luds = new List<Util_GeoSimple>();
            foreach (Util_Geo ud in lud)
            {
                luds.Add(new Util_GeoSimple()
                {
                    id_Util_Geo = ud.id_Util_Geo,
                    id_Departement = ud.id_Departement,
                    name_Departement = ud.Ref_Departement.Description,
                    idU = ud.idU,
                    id_Region = ud.Ref_Departement.Ref_Region.id_Region,
                    name_Region = ud.Ref_Departement.Ref_Region.Description
                });
            }
            lud = null;
            return luds;
        }
    }
}