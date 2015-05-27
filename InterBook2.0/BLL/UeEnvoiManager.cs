﻿using InterBook2._0.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class UeEnvoiManager
    {
        public static void InsertLine(UE_envoi uee)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            SessionManager.Current.ws.InsertLine(uee);
        }
    }
}