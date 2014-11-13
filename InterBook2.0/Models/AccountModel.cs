using InterBook2._0.DTO;
using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Models
{
    public class CriterionsModel : ModelBase
    {

    }

    public class SignUpModel : ModelBase
    {
        public Util u { get; set; }
    }

    public class SignInModel : ModelBase
    {
        [Required(ErrorMessageResourceName = "RequiredCivilite", ErrorMessageResourceType = typeof(Errors))]
        [Range(1, 3, ErrorMessageResourceName = "InvalidCivilite", ErrorMessageResourceType = typeof(Errors))]
        public byte Civilite { get; set; }

        [Required(ErrorMessageResourceName = "RequiredNom", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression("^([a-zA-Z' äàâæçèéêëîïôùûüœ-]{1,50})$", ErrorMessageResourceName = "InvalidNom", ErrorMessageResourceType = typeof(Errors))]
        public string Nom { get; set; }

        [Required(ErrorMessageResourceName = "RequiredPrenom", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression("^([a-zA-Z' äàâæçèéêëîïôùûüœ-]{1,50})$", ErrorMessageResourceName = "InvalidPrenom", ErrorMessageResourceType = typeof(Errors))]
        public string Prenom { get; set; }

        [Required(ErrorMessageResourceName = "RequiredEmail", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Errors))]
        //[Remote("EmailExists", "Home")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredPassword", ErrorMessageResourceType = typeof(Errors))]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "RequiredPasswordConfirm", ErrorMessageResourceType = typeof(Errors))]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}