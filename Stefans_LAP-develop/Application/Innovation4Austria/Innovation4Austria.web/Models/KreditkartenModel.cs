using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class KreditkartenModel :RechnungsModel
    {
        public List<KreditkartenArtModel> KreditkartenBezeichnung { get; set; }

        public int KreditkartenArtId { get; set; }


        public string KreditKartenNummer { get; set; }


        public int GültigBisMonat { get; set; }


        public int GültigBisJahr { get; set; }


        public string Vorname { get; set; }


        public string Nachname { get; set; }


        public string Prüfnummer { get; set; }


        public decimal Rechnungsbetrag { get; set; }


        public int Buchung_id { get; set; }
    }
}