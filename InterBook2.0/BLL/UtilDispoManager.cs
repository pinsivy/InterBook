using InterBook2._0.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilDispoManager
    {
        public static List<Util_DispoSimple> GetUtilDispoByIdu(int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            List<Util_DispoSimple> luds = SessionManager.Current.ws.GetUtilDispoByIdu(idu);
            
            return luds;
        }

        public static Util_DispoSimple ajoutDispo(DateTime dDispo, int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            Util_DispoSimple uds = SessionManager.Current.ws.GetUtilDispoByDateIdu(dDispo, idu);

            //Modifier ou ajouter
            if (uds != null)
            {
                if (uds.id_Ref_Dispo == 1)
                {
                    uds.id_Ref_Dispo = 3;
                }
                else if (uds.id_Ref_Dispo == 3)
                {
                    uds.id_Ref_Dispo = 1;
                }
            }
            else
            {
                uds = new Util_DispoSimple()
                {
                    dDispo = dDispo,
                    idU = idu,
                    id_Ref_Dispo = 1
                };
            }
            SessionManager.Current.ws.InsertLine_Util_Dispo(uds);

            return uds;
        }

        public static Util_DispoSimple ajoutReserve(DateTime dDispo, int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            Util_DispoSimple uds = SessionManager.Current.ws.GetUtilDispoByDateIdu(dDispo, idu);

            //Modifier ou ajouter
            if (uds != null)
            {
                uds.id_Ref_Dispo = 2;
            }
            else
            {
                uds = new Util_DispoSimple()
                {
                    dDispo = dDispo,
                    idU = idu,
                    id_Ref_Dispo = 2
                }; ;
            }
            SessionManager.Current.ws.InsertLine_Util_Dispo(uds);



            return uds;
        }
    }
}