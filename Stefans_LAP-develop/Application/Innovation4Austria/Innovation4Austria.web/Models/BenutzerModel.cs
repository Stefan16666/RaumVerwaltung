using Innovation4Austria.web.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovation4Austria.web.Models
{
    public class BenutzerModel
    {
        public int Fa_id { get; set; }

        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Remote("EmailFrei", "Validierung", ErrorMessage = "Email Adresse bereits vergeben")]
        public string Emailadresse { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",ErrorMessageResourceType =typeof(Validierungen),ErrorMessageResourceName = Validation.NAMENSKONTROLLE)]
        public string  Nachname { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessageResourceType = typeof(Validierungen), ErrorMessageResourceName = Validation.NAMENSKONTROLLE)]
        public string Vorname { get; set; }

        public bool Aktiv { get; set; }
    }
}