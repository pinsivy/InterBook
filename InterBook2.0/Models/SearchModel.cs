﻿using InterBook2._0.App_GlobalResources;
using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterBook2._0.Models
{
    public class SearchModel : ModelBase
    {
        public List<Util> lu { get; set; }

        [DisplayName("Quoi ?")]
        [Required(ErrorMessageResourceName = "RequiredProfession", ErrorMessageResourceType = typeof(Errors))]
        public string Profession { get; set; }

        [DisplayName("Où ?")]
        [Required(ErrorMessageResourceName = "RequiredVille", ErrorMessageResourceType = typeof(Errors))]
        public string Ville { get; set; }


        [DisplayName("De")]
        public DateTime Debut { get; set; }

        [DisplayName("à")]
        public DateTime Fin { get; set; }
    }
}