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

            List<Util_Dispo> lud = SessionManager.Current.ws.GetUtilDispoByIdu(idu);
            List<Util_DispoSimple> luds = new List<Util_DispoSimple>();
            foreach(Util_Dispo ud in lud)
            {
                luds.Add(new Util_DispoSimple()
                {
                    dDispo = ud.dDispo,
                    idU = ud.idU,
                    id_Ref_Dispo = ud.id_Ref_Dispo
                });
            }
            lud = null;
            return luds;
        }

        public static Util_DispoSimple ajoutDispo(DateTime dDispo, int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            Util_Dispo ud = SessionManager.Current.ws.GetUtilDispoByDateIdu(dDispo, idu);

            Util_DispoSimple uds = new Util_DispoSimple()
            {
                dDispo = dDispo,
                idU = idu,
                id_Ref_Dispo = 1
            };

            //Modifier ou ajouter
            if (ud != null)
            {
                uds.id_Util_Dispo = ud.id_Util_Dispo;
                if (ud.id_Ref_Dispo == 1)
                {
                    ud.id_Ref_Dispo = uds.id_Ref_Dispo = 3;
                }
                else if (ud.id_Ref_Dispo == 3)
                {
                    ud.id_Ref_Dispo = uds.id_Ref_Dispo = 1;
                }
            }
            else
            {
                ud = new Util_Dispo()
                {
                    dDispo = dDispo,
                    idU = idu,
                    id_Ref_Dispo = 1
                };
            }
            SessionManager.Current.ws.InsertLine(ud);



            return uds;
        }

        public static Util_DispoSimple ajoutReserve(DateTime dDispo, int idu)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            Util_Dispo ud = SessionManager.Current.ws.GetUtilDispoByDateIdu(dDispo, idu);

            Util_DispoSimple uds = new Util_DispoSimple()
            {
                dDispo = dDispo,
                idU = idu,
                id_Ref_Dispo = 1
            };

            //Modifier ou ajouter
            if (ud != null)
            {
                uds.id_Util_Dispo = ud.id_Util_Dispo;
                ud.id_Ref_Dispo = uds.id_Ref_Dispo = 2;
            }
            else
            {
                ud = new Util_Dispo()
                {
                    dDispo = dDispo,
                    idU = idu,
                    id_Ref_Dispo = 2
                }; ;
            }
            SessionManager.Current.ws.InsertLine(ud);



            return uds;
        }
    }
}