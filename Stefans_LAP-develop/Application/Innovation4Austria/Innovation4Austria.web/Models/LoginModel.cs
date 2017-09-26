using Innovation4Austria.web.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    /// <summary>
    /// Modell für das Einloggen
    /// </summary>
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false,ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.REQUIRED)]
        [EmailAddress(ErrorMessageResourceName = Validation.DATATYPE_MAIL, ErrorMessageResourceType = typeof(Validierungen))]
        public string Emailadresse { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType =typeof(Validierungen), ErrorMessageResourceName = Validation.REQUIRED)]            
        [DataType(DataType.Password)]
        public string Passwort { get; set; }

        public int Fa_id { get; set; }
    }
}