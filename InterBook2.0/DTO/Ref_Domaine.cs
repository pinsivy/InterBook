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
    
    public partial class Ref_Domaine
    {
        public int id_Domaine { get; set; }
        public string Domaine { get; set; }
        public Nullable<int> id_Declinaison_Culture { get; set; }
    
        public virtual Ref_Declinaison_Culture Ref_Declinaison_Culture { get; set; }
    }
}
