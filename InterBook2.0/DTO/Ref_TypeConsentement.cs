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
    
    public partial class Ref_TypeConsentement
    {
        public Ref_TypeConsentement()
        {
            this.Util_Consentement = new HashSet<Util_Consentement>();
        }
    
        public int id_Ref_TypeConsentement { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Util_Consentement> Util_Consentement { get; set; }
    }
}
