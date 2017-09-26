using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class RechnungsDetailModel
    {
        public string Buchungs_ID { get; set; }

        public DateTime Buchungsdatum { get; set; }

        public decimal Preis { get; set; }

        public string RaumNummer { get; set; }
    }
}