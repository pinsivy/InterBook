using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Ref_FromSimple
    {
        public int id_From { get; set; }
        public string Description { get; set; }
        public Nullable<int> id_Declinaison_Culture { get; set; }
    }
}
