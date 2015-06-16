using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilMessageManager
    {
        public static void InsertLine(Util_MessageSimple utilmessage)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            SessionManager.Current.ws.InsertLine_Util_Message(utilmessage);
        }

        public static List<Util_MessageSimple> GetUtilMessageByIdutoIdufrom(int iduFrom, int iduTo)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();


            List<Util_MessageSimple> lums = SessionManager.Current.ws.GetUtilMessageByIdutoIdufrom(iduFrom, iduTo);

            return lums;
        }

        public static List<Util_ContactSimple> GetUtilContactByIdufrom(int iduFrom)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetUtilContactByIdufrom(iduFrom);
        }

        public static Util_MessageSimple addMessage(int iduFrom, int iduTo, string message)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            Util_MessageSimple um = new Util_MessageSimple()
            {
                dMessage = DateTime.Now,
                idUFrom = iduFrom,
                idUTo = iduTo,
                message = message,
            };
            SessionManager.Current.ws.InsertLine_Util_Message(um);
            return um;
        }
    }
}