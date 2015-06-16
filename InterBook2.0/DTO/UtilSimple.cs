using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class UtilSimple
    {
        public int IdU { get; set; }
        public Nullable<int> id_Declinaison_Culture { get; set; }
        public Nullable<int> id_From { get; set; }
        public string mdp { get; set; }
        public Nullable<System.Guid> uid { get; set; }
        public Nullable<int> idu_Email { get; set; }
        public Nullable<int> idu_Postal { get; set; }
        public Nullable<int> idu_Telmobile { get; set; }
        public Nullable<System.DateTime> dCrea { get; set; }
        public Nullable<System.DateTime> dMAJ { get; set; }
        public Nullable<bool> particulier { get; set; }
        public Nullable<int> id_Util_Info { get; set; }
        public Nullable<int> id_Util_Info_Entreprise { get; set; }
    }
}
