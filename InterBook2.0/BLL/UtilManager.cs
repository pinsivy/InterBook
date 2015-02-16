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
            IBWS ws = new IBWS();
            ws.InsertLine(util);

            if (storeInSession)
                SessionManager.Current.Util = util;
        }

        public static Util GetUtilByIdU(int idU)
        {
            IBWS ws = new IBWS();
            return ws.GetUtilByIdu(idU);
        }

        public static Util GetUtilByUid(Guid? uid)
        {
            IBWS ws = new IBWS();
            return ws.GetUtilByUid(uid);
        }

        public static String GetIduByUtil(List<Util> u)
        {
            return u[0].IdU.ToString();
        }

        public static Util GetUtilByEmail(string email)
        {
            IBWS ws = new IBWS();
            return ws.GetUtilByEmail(email);
        }

        public static Util GetUtilByEmailMdp(string email, string mdp)
        {
            IBWS ws = new IBWS();
            return ws.GetUtilByEmailMdp(email, mdp);
        }

        public static List<Ref_Profession> GetProfessions(string debut, string maxRows)
        {
            IBWS ws = new IBWS();
            return ws.GetProfessions(debut, maxRows);
        }
    }
}