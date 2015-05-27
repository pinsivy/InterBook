using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Util_MessageSimple
    {
        public int id_Util_Message { get; set; }
        public Nullable<System.DateTime> dMessage { get; set; }
        public Nullable<int> idUFrom { get; set; }
        public Nullable<int> idUTo { get; set; }
        public String message { get; set; }
    }
}
