using InterBook2._0.BLL;
using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterBook2._0.Models
{
    public class DashboardModel : ModelBase
    {
        public Util u { get { return SessionManager.Current.Util; } }

        public List<Util_ContactSimple> luc { get; set; }

        [DisplayName("Departement")]
        public IEnumerable<GroupedSelectListItem> DepartementList { get; set; }

        public List<string> SubmittedDepartement { get; set; }
    }
}