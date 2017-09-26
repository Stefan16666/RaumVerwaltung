using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class DashboardModel
    {
        public List<BenutzerModel> AlleMitarbeiter { get; set; }

        public List<BuchungsAnzeigeModel> AlleBuchungen { get; set; }

        public List<RechnungsModel> AlleRechnungen { get; set; }
    }
}