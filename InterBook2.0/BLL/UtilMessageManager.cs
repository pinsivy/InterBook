using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilMessageManager
    {
        public static void InsertLine(Util_Message utilmessage)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine(utilmessage);
        }

        public static List<Util_MessageSimple> GetUtilMessageByIdutoIdufrom(int iduFrom, int iduTo)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();


            List<Util_Message> lum = SessionManager.Current.ws.GetUtilMessageByIdutoIdufrom(iduFrom, iduTo);
            List<Util_MessageSimple> lums = new List<Util_MessageSimple>();
            foreach (Util_Message um in lum)
            {
                lums.Add(new Util_MessageSimple()
                {
                    id_Util_Message = um.id_Util_Message,
                    dMessage = um.dMessage,
                    idUFrom = um.idUFrom,
                    idUTo = um.idUTo,
                    message = um.message
                });
            }
            lum = null;
            return lums;
        }

        public static List<Util_Contact> GetUtilContactByIdufrom(int iduFrom)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilContactByIdufrom(iduFrom);
        }

        public static Util_Message addMessage(int iduFrom, int iduTo, string message)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            Util_Message um = new Util_Message()
            {
                dMessage = DateTime.Now,
                idUFrom = iduFrom,
                idUTo = iduTo,
                message = message,
            };
            SessionManager.Current.ws.InsertLine(um);
            return um;
        }
    }
}