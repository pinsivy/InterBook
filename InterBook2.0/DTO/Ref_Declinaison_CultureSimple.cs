using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Ref_Declinaison_CultureSimple
    {
        public int id_Declinaison_Culture { get; set; }
        public Nullable<int> id_Declinaison { get; set; }
        public Nullable<int> id_Culture { get; set; }
        public string id_From_Defaut { get; set; }
        public string EmailFromSupport { get; set; }
    }
}
