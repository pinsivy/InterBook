using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterBook2._0.Models
{
    public class UserModel : ModelBase
    {
        public Util u { get; set; }

        public String DebutReserve { get; set; }
        public String FinReserve { get; set; }
    }
}