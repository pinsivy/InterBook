using InterBook2._0.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UeEnvoiManager
    {
        public static void InsertLine(UE_envoiSimple uee)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            //Changer ue_envoi en simple

            SessionManager.Current.ws.InsertLine_UE_envoi(uee);
        }

        public static void InsertLine(UE_envoi uee)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            InterBookEntities _db = new InterBookEntities();

            _db.Entry(uee).State = uee.id_UE_envoi == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
            SessionManager.Current.ws.Save();
        }
    }
}