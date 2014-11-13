using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UtilPostalManager
    {
        public static void InsertLine(Util_Postal utilpostal)
        {
            IBWS ws = new IBWS();
            ws.InsertLine(utilpostal);
        }
    }
}