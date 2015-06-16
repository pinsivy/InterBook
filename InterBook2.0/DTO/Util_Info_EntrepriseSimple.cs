using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Util_Info_EntrepriseSimple
    {
        public int id_Util_Info_Entreprise { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Ville { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string APE { get; set; }
        public Nullable<int> Logo { get; set; }
        public string Siret { get; set; }
        public string Siren { get; set; }
        public string SiteWeb { get; set; }
    }
}
