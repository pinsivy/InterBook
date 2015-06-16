using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.DTO
{
    public partial class ReservationSimple
    {
        public int id_Reservation { get; set; }
        public Nullable<int> idUEntreprise { get; set; }
        public Nullable<System.DateTime> debut { get; set; }
        public Nullable<System.DateTime> fin { get; set; }
        public Nullable<int> id_EtatReservation { get; set; }
        public Nullable<int> idUEmploye { get; set; }
    }
}
