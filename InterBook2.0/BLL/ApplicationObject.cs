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
        public static List<Ref_DomaineSimple> DomainesInfos;
        public static List<Ref_CultureSimple> CulturesInfos;
        public static List<Ref_Declinaison_CultureSimple> DeclinaisonCultureInfos;
    }
}
