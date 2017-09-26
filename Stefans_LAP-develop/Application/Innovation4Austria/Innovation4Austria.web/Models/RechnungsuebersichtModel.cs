using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class RechnungsuebersichtModel
    {
        public int Monat { get; set; }

        public int Jahr { get; set; }

        public string Bezeichnung { get; set; }

        public bool schonBezahlt { get; set; }
    }
}