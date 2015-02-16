using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.Models
{
    public class ConfirmModel : ModelBase
    {
        public Guid? Uid { get; set; }
        public short? Idm { get; set; }

        public bool IsValid()
        {
            return Uid.HasValue && Idm.HasValue;
        }
    }
}