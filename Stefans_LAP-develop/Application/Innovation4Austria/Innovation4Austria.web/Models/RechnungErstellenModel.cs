using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class RechnungErstellenModel
    {
        public int Rg_Id { get; set; }

        public string Firmenname { get; set; }

        public string Firmenstrasse { get; set; }

        public string FirmenstrassenNummer { get; set; }

        public string FirmenOrt { get; set; }

        public string FirmenPlz { get; set; }

        public DateTime Datum { get; set; }

        public int Raumnummer { get; set; }

        public int Dauer { get; set; }       

        public decimal Gesamtpreis { get; set; }

        public decimal Steuerbetrag { get; set; }

        public List<RechnungsDetailModel> RechnungsDetails { get; set; }
    }
}