using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.BLL
{
    public static class ApplicationManager
    {
        public static void SetDomaineDictionnary()
        {
            //on va chercher les résultats
            ApplicationObject.DomainesInfos = DeclinaisonCultureManager.SelectAllRefDomaine();
        }

        public static void SetCultureDictionnary()
        {
            //on va chercher les résultats
            ApplicationObject.CulturesInfos = DeclinaisonCultureManager.SelectAllRefCulture();
        }

        public static void SetDeclinaisonCultureDictionnary()
        {
            //on va chercher les résultats
            ApplicationObject.DeclinaisonCultureInfos = DeclinaisonCultureManager.SelectAllRefDeclinaisonCulture();
        }
    }
}
