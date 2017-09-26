using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class ProfilAnzeigeModel
    {
        public BenutzerVerwaltungsModel derMitarbeiter { get; set; }

        public PasswortVerwaltungsModel anderesPasswort { get; set; }    
    }
}