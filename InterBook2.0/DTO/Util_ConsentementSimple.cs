using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class Util_ConsentementSimple
    {
        public int id_Util_Consentement { get; set; }
        public Nullable<int> id_TypeConsentement { get; set; }
        public Nullable<int> valeur { get; set; }
        public Nullable<int> id_Mailing { get; set; }
        public Nullable<int> idu { get; set; }
        public Nullable<System.DateTime> dRecueil { get; set; }
    }
}
