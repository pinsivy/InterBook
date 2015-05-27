using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterBook2._0.Models
{
    public class DashboardModel : ModelBase
    {
        //public Util u { return SessionManager.Current.Util }
        public Util u { get; set; }

        public List<Util_Contact> luc { get; set; }
    }
}