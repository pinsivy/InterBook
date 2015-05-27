using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilFavorisManager
    {
        public static void InsertLine(Util_Favoris UtilFavoris)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine(UtilFavoris);
        }

        public static Util_Favoris GetUtilFavorisByIduIduEnt(int idu, int iduEntr)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilFavorisByIduIduEnt(idu, iduEntr);
        }
    }
}