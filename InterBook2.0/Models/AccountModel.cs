using InterBook2._0.App_GlobalResources;
using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterBook2._0.Models
{
    public class SignUpEModel : ModelBase
    {
        [Required(ErrorMessageResourceName = "RequiredCivilite", ErrorMessageResourceType = typeof(Errors))]
        [Range(1, 2, ErrorMessageResourceName = "InvalidCivilite", ErrorMessageResourceType = typeof(Errors))]
        public byte Civilite { get; set; }

        [Required(ErrorMessageResourceName = "RequiredNom", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression("^([a-zA-Z' äàâæçèéêëîïôùûüœ-]{1,50})$", ErrorMessageResourceName = "InvalidNom", ErrorMessageResourceType = typeof(Errors))]
        public String Nom { get; set; }

        [Required(ErrorMessageResourceName = "RequiredPrenom", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression("^([a-zA-Z' äàâæçèéêëîïôùûüœ-]{1,50})$", ErrorMessageResourceName = "InvalidPrenom", ErrorMessageResourceType = typeof(Errors))]
        public String Prenom { get; set; }

        public DateTime? DateDeNaissance { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessageResourceName = "RequiredPrenom", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression("^([0-9]{10})$", ErrorMessageResourceName = "InvalidPrenom", ErrorMessageResourceType = typeof(Errors))]
        public string NumTel { get; set; }

        [Required(ErrorMessageResourceName = "RequiredEmail", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Errors))]
        public string Email { get; set; }


        [Required(ErrorMessageResourceName = "RequiredNom", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression("^([a-zA-Z' äàâæçèéêëîïôùûüœ-]{1,50})$", ErrorMessageResourceName = "InvalidNom", ErrorMessageResourceType = typeof(Errors))]
        public String NomEntreprise { get; set; }

        public String NumTelEntreprise { get; set; }
        public String EmailEntreprise { get; set; }
        public String VilleEntreprise { get; set; }
        public String Fax { get; set; }
        public String APE { get; set; }
        public string FileUploadPhoto { get; set; }
        public String Siret { get; set; }
        public String Siren { get; set; }
        public String SiteWeb { get; set; }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(Prenom) && !String.IsNullOrEmpty(Nom);
        }


    }

    public class SignInModel : ModelBase
    {
        public Util u { get; set; }
    }

    public class SignUpModel : ModelBase
    {
        public SignUpModel() { 
            ExperienceList = new List<SelectListItem>();
        }

        [Required(ErrorMessageResourceName = "RequiredCivilite", ErrorMessageResourceType = typeof(Errors))]
        [Range(1, 2, ErrorMessageResourceName = "InvalidCivilite", ErrorMessageResourceType = typeof(Errors))]
        public byte Civilite { get; set; }

        [Required(ErrorMessageResourceName = "RequiredNom", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression("^([a-zA-Z' äàâæçèéêëîïôùûüœ-]{1,50})$", ErrorMessageResourceName = "InvalidNom", ErrorMessageResourceType = typeof(Errors))]
        public String Nom { get; set; }

        [Required(ErrorMessageResourceName = "RequiredPrenom", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression("^([a-zA-Z' äàâæçèéêëîïôùûüœ-]{1,50})$", ErrorMessageResourceName = "InvalidPrenom", ErrorMessageResourceType = typeof(Errors))]
        public String Prenom { get; set; }

        public DateTime? DateDeNaissance { get; set; }

        [Required(ErrorMessageResourceName = "RequiredVille", ErrorMessageResourceType = typeof(Errors))]
        public String Ville { get; set; }

        public bool PermisA { get; set; }
        public bool PermisB { get; set; }
        public bool PermisC { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessageResourceName = "RequiredPrenom", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression("^([0-9]{10})$", ErrorMessageResourceName = "InvalidPrenom", ErrorMessageResourceType = typeof(Errors))]
        public string NumTel { get; set; }

        [Required(ErrorMessageResourceName = "RequiredEmail", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Errors))]
        //[Remote("EmailExists", "Home")]
        public string Email { get; set; }

        public string FileUploadCV { get; set; }

        public string FileUploadPhoto { get; set; }

        public string Motivation { get; set; }

        [DisplayName("Experience")]
        public List<SelectListItem> ExperienceList { get; set; }

        public string Experience { get; set; }

        [DisplayName("Departement")]
        public IEnumerable<GroupedSelectListItem> DepartementList { get; set; }

        public List<string> SubmittedDepartement { get; set; }

        public Guid uid { get; set; }

        public List<string> lProfession { get; set; }

        public string lDate { get; set; }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(Prenom) && !String.IsNullOrEmpty(Nom);
        }


    }
}