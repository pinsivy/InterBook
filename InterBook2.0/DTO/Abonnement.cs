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
    
    public partial class Abonnement
    {
        public int id_Abonnement { get; set; }
        public Nullable<System.DateTime> dCrea { get; set; }
        public Nullable<System.DateTime> dFin { get; set; }
        public Nullable<int> idU { get; set; }
        public string HiPay_transaction_id { get; set; }
        public string HiPay_subscriber_reference { get; set; }
    
        public virtual Util Util { get; set; }
    }
}
