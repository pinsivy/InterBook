using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public class ReservationManager
    {
        public static void InsertLine(ReservationSimple r)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            SessionManager.Current.ws.InsertLine_Reservation(r.id_Reservation.ToString(), r.idUEntreprise.ToString(), r.id_EtatReservation.ToString(), r.idUEmploye.ToString());
        }

        public static ReservationSimple GetReservationByIdr(int idr)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();

            ReservationSimple rs = SessionManager.Current.ws.GetReservationByIdr(idr);

            return rs;
        }
    }
}