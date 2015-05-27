using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Util_DispoSimple
    {
        public int id_Util_Dispo { get; set; }
        public Nullable<System.DateTime> dDispo { get; set; }
        public Nullable<int> idU { get; set; }
        public Nullable<int> id_Ref_Dispo { get; set; }
    }
}
