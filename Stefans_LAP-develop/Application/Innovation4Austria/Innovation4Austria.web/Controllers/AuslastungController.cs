using Innovation4Austria.logic;
using Innovation4Austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovation4Austria.web.Controllers
{


    public class AuslastungController : Controller
    {

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Authorize]
        [HttpGet]
        public ActionResult RaumAuslastung()
        {
            List<AuslastungsModel> AuslastungDerRaueme = new List<AuslastungsModel>();
            List<Raum> alleRaeume = new List<Raum>();
            alleRaeume = RaumVerwaltung.alleRaeume();
            foreach (var raum in alleRaeume)
            {
                AuslastungsModel model = new AuslastungsModel()
                {
                    Raumbezeichnung = raum.Bezeichnung,
                    AnzahlTage = BuchungsVerwaltung.AnzahlTage(raum.Id)
                };
                if (model.AnzahlTage>0)
                {
                    AuslastungDerRaueme.Add(model);
                }
                
            }

            return View(AuslastungDerRaueme);
        }
    }
}