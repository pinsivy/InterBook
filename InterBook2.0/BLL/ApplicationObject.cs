using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.BLL
{
    [Serializable]
    public static class ApplicationObject
    {
        public static List<Ref_Domaine> DomainesInfos;
        public static List<Ref_Culture> CulturesInfos;
        public static List<Ref_Declinaison_Culture> DeclinaisonCultureInfos;
    }
}
