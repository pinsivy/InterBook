using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Util_ContactSimple
    {
        public int id_Util_Contact { get; set; }
        public Nullable<int> iduFrom { get; set; }
        public Nullable<int> iduTo { get; set; }
    }
}
