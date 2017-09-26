using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class FirmenModel
    {
        public int Id { get; set; }

        public string Bezeichnung { get; set; }

        public string Strasse { get; set; }

        public string Nummer { get; set; }

        public string Plz { get; set; }

        public string Ort { get; set; }

        public bool aktiv { get; set; }

        public BenutzerAnlegenModel NeuerBenutzer { get; set; }

    }
}