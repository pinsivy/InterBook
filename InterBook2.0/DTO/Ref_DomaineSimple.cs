using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Ref_DomaineSimple
    {
        public int id_Domaine { get; set; }
        public string Domaine { get; set; }
        public Nullable<int> id_Declinaison_Culture { get; set; }
    }
}
