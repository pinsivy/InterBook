using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilGeoManager
    {
        public static void InsertLine(Util_GeoSimple UtilInfo)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine_Util_Geo(UtilInfo);
        }

        public static List<Util_GeoSimple> GetUtilGeoByIdu(int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            List<Util_GeoSimple> luds = SessionManager.Current.ws.GetUtilGeoByIdu(idu);
            
            return luds;
        }
    }
}