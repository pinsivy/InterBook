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
    
    public partial class Util_Email
    {
        public Util_Email()
        {
            this.Utils = new HashSet<Util>();
        }
    
        public int idu_Email { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> dCrea { get; set; }
    
        public virtual ICollection<Util> Utils { get; set; }
    }
}