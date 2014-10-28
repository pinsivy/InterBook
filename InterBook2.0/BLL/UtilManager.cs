using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilManager
    {
        public static Util GetUtilByIdU(int idU)
        {
            return null;
        }

        public static String GetIduByUtil(List<Util> u)
        {
            return u[0].IdU.ToString();
        }
    }
}