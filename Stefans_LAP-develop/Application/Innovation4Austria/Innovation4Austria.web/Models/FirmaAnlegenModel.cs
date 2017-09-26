using Innovation4Austria.web.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class FirmaAnlegenModel
    {

        [Required]
        public string Bezeichnung { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z-\s]{1,40}$", ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.NAMENSKONTROLLE)]
        public string Strasse { get; set; }

        [Required]       
        public string Nummer { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1,5}$", ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.POSTLEITZAHL)]
        public string Plz { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.NAMENSKONTROLLE)]
        public string Ort { get; set; }

        
    }
}