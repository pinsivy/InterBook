using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Util_InfoSimple
    {
        public int id_Util_Info { get; set; }
        public Nullable<int> photo { get; set; }
        public Nullable<int> cv { get; set; }
        public string numTel { get; set; }
        public Nullable<System.DateTime> dNaissance { get; set; }
        public Nullable<int> experience { get; set; }
        public string motivation { get; set; }
        public Nullable<bool> permisA { get; set; }
        public Nullable<bool> permisB { get; set; }
        public Nullable<bool> permisC { get; set; }
    }
}
