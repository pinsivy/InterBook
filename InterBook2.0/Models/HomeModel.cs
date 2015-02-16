using InterBook2._0.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InterBook2._0.Models
{
    public class HomeModel : ModelBase
    {
        [Required(ErrorMessageResourceName = "RequiredProfession", ErrorMessageResourceType = typeof(Errors))]
        public string Profession { get; set; }

        [Required(ErrorMessageResourceName = "RequiredVille", ErrorMessageResourceType = typeof(Errors))]
        public string Ville { get; set; }

        public DateTime? Debut { get; set; }

        public DateTime? Fin { get; set; }
    }
}