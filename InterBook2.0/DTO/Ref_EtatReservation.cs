//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterBook2._0.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ref_EtatReservation
    {
        public Ref_EtatReservation()
        {
            this.Reservations = new HashSet<Reservation>();
        }
    
        public int id_Ref_EtatReservation { get; set; }
        public string description { get; set; }
    
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
