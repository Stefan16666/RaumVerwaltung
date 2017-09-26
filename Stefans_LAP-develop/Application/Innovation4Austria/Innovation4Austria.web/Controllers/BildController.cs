using Innovation4Austria.logic;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovation4Austria.web.Controllers
{
    public class BildController : BasisController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        public ActionResult IconZuAusstattung(int id)
        {                  
   
            log.Info("Bild - iconZuAusstattung - GET");
           
            string contentType = "image/jpeg";
            byte[] bilddaten = BildVerwaltung.LadeAusstattung(id).bilddaten;
            if (bilddaten==null)
            {
                log.Warn("BildController - IconZuAusstattung - bilddaten nicht gefunden");
            }
           
            return new FileContentResult(bilddaten, contentType);

        }

        public ActionResult BildZuRaum(int id)
        {
            log.Info("Bild - iconZuAusstattung - GET");

            string contentType = "image/jpeg";
            byte[] bilddaten = BildVerwaltung.LadeBild(id).bilddaten;
            if (bilddaten == null)
            {
                log.Warn("BildController - BildZuRaum - bilddaten nicht gefunden");
            }

            return new FileContentResult(bilddaten, contentType);

        }
        }
}