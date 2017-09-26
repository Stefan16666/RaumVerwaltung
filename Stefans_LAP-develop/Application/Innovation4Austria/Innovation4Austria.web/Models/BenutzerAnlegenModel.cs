using Innovation4Austria.web.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class BenutzerAnlegenModel:BenutzerModel
    {
        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.PASSWORT_NORM)]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.REQUIRED)]
        [DataType(DataType.Password)]
        [DisplayName("Passwort")]
        public string NeuesPasswort { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.REQUIRED)]
        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.PASSWORT_NORM)]
        [DataType(DataType.Password)]
        [DisplayName("Passwort wiederholen")]
        [Compare("NeuesPasswort",ErrorMessage ="Passwort stimmt nicht überein")]
        public string NeuesPasswortBestätigung { get; set; }
    }
}