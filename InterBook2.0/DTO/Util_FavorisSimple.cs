using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Util_FavorisSimple
    {
        public int id_Util_Favoris { get; set; }
        public Nullable<int> idU { get; set; }
        public Nullable<int> idUEntreprise { get; set; }
        public Nullable<bool> actif { get; set; }
    }
}
