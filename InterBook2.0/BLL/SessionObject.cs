using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    [Serializable]
    public class SessionObject
    {
        public Util Util { get; set; }

        public IBWS ws { get; set; }

        public string CurrentPage { get; set; }

        public string Notification { get; set; }

        public int Id_Declinaison_Culture { get; set; }

        public CultureInfo CurrentUICulture { get; set; }

        public bool UtilExists()
        {
            return this.Util != null && this.Util.IdU > 0;
        }
    }
}