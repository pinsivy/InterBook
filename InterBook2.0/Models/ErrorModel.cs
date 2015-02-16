using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Models
{
    public class ErrorModel : ModelBase
    {
        public bool IsError500 { get; set; }
        public string Description { get; set; }
    }
}
