using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Ref_VilleSimple
    {
        public int id_Ville { get; set; }
        public string Description { get; set; }
        public string cp { get; set; }
        public string insee { get; set; }
        public string article { get; set; }
        public string ville { get; set; }
        public Nullable<int> id_Region { get; set; }
        public string id_Departement { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }
}
