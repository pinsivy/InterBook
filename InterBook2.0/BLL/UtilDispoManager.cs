using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilDispoManager
    {
        public static List<Util_Dispo> GetUtilDispoByIdu(int idu)
        {
            IBWS ws = new IBWS();
            return ws.GetUtilDispoByIdu(idu);
        }

        public static Util_Dispo ajoutDispo(DateTime dDispo, int idu)
        {
            IBWS ws = new IBWS();
            Util_Dispo ud = ws.GetUtilDispoByDateIdu(dDispo, idu);

            //Modifier ou ajouter
            if (ud != null)
            {
                if (ud.id_Ref_Dispo == 1)
                    ud.id_Ref_Dispo = 3;
                else if (ud.id_Ref_Dispo == 3)
                    ud.id_Ref_Dispo = 1;
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
            ws.InsertLine(ud);
            return ud;
        }
    }
}