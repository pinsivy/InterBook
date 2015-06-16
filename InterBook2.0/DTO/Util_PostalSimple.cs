using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Util_PostalSimple
    {
        public int idu_Postal { get; set; }
        public Nullable<int> id_Civilite { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string adresse1 { get; set; }
        public string adresse2 { get; set; }
        public string cp { get; set; }
        public Nullable<int> id_Ville { get; set; }
        public Nullable<int> id_Pays { get; set; }
        public Nullable<System.DateTime> dCrea { get; set; }
    }
}
