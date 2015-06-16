using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class UE_envoiSimple
    {
        public int id_UE_envoi { get; set; }
        public Nullable<int> id_mailing { get; set; }
        public Nullable<int> idu_email { get; set; }
        public Nullable<int> idu { get; set; }
        public Nullable<System.DateTime> dEnvoi { get; set; }
    }
}
