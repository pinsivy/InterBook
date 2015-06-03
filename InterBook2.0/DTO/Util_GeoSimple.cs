using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Util_GeoSimple
    {
        public int id_Util_Geo { get; set; }
        public String id_Departement { get; set; }
        public String name_Departement { get; set; }
        public Nullable<int> id_Region { get; set; }
        public String name_Region { get; set; }
        public Nullable<int> idU { get; set; }
    }
}
