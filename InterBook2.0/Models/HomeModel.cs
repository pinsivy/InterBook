using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}