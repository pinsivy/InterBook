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
    
    public partial class Util_Message
    {
        public int id_Util_Message { get; set; }
        public Nullable<int> idUFrom { get; set; }
        public Nullable<int> idUTo { get; set; }
        public string message { get; set; }
        public Nullable<System.DateTime> dMessage { get; set; }
    
        public virtual Util Util { get; set; }
        public virtual Util UtilTo { get; set; }
    }
}
