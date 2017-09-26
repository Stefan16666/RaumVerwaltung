using Innovation4austria.authentication;
using Innovation4Austria.authentication;
using Innovation4Austria.logic;
using Innovation4Austria.web.AppCode;
using Innovation4Austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Verwaltung;

namespace Innovation4Austria.web.Controllers
{
    public class Innovation4AustriaMitarbeiterController : BasisController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly i4aMembershipProvider membershipProvider = new i4aMembershipProvider();

        private static readonly i4aRoleProvider roleProvider = new i4aRoleProvider();
        /// <summary>
        /// Lädt alle Firmen
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult FirmenAuflistung()
        {
            log.Info("Innovation4AutriaController - FirmenAuflistung - Get");

            List<FirmenModel> FirmenUI = AutoMapper.Mapper.Map<List<FirmenModel>>(FirmenVerwaltung.LadeAlleFirmen());
            if (FirmenUI != null)
            {
                log.Error("Innovatation4AustriaController - firmenAuflistung - keine Firmen gefunden");
            }
            return View(FirmenUI);
        }

        /// <summary>
        /// Hier werden die Daten einer Firma übergeben und zur Aktualisierung an die BL weitergeleitet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult FirmenAuflistung(FirmenModel model)
        {
            log.Info("Innovatation4AustriaController - FirmenAuflistung - Post");

            if (ModelState.IsValid)
            {
                if (FirmenVerwaltung.FirmaAktualisierung(model.Id, model.Bezeichnung, model.aktiv, model.Nummer, model.Ort, model.Plz, model.Strasse))
                {
                    log.Info("Innovation4AustriaMitarbeiter - FirmenAuflistung - Post - Firma aktualisiert");
                }
                else
                {
                    log.Warn("Innovation4AustriaMitarbeiter - FirmenAuflistung - Post - Firma aktualisiert");
                }
            }
            return RedirectToAction("FirmenAuflistung");
        }

        /// <summary>
        /// alle Mitarbeiter einer Firma werden zur View geschickt
        /// </summary>
        /// <param name="fa_id"></param>
        /// <returns></returns>
        
        [Authorize]
        [HttpGet]
        public ActionResult MitarbeiterBearbeiten(int fa_id)
        {

            log.Info("Innovatation4AustriaController - MitarbeiterBearbeiten - GET");

            List<BenutzerModel> alleBenutzer = AutoMapper.Mapper.Map<List<BenutzerModel>>(BenutzerVerwaltung.LadeMitarbeiterEinerFirma(fa_id));
            if (alleBenutzer == null)
            {
                log.Error("Innovatation4AustriaController - firmenAuflistung - keine Firmen gefunden");
            }
            return View(alleBenutzer);
        }

        /// <summary>
        ///  Der Mitarbeiter der wird übergeben und die Daten werden zur Aktualisierung an die BL weitergeleitet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult MitarbeiterBearbeiten(BenutzerModel model)
        {
            log.Info("Innovatation4AustriaController - MitarbeiterBearbeiten - Post");


            if (BenutzerVerwaltung.AktualisiereMitarbeiterEinerFirma(model.Id, model.Emailadresse, model.Vorname, model.Vorname, model.Aktiv))
            {
                log.Error("Innovatation4AustriaController - AktualisiereMitarbeiterEinerFirma - Speichern hat nicht geklappt");
            }
            return RedirectToAction("FirmenAuflistung");

        }

        /// <summary>
        /// Daten von einem neuen Mitarbeiter werden an die BL weitergeleitet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult MitarbeiterAnlegen(BenutzerAnlegenModel model)
        {
            if (ModelState.IsValid)
            {
                log.Info("Innovatation4AustriaController - MitarbeiterAnlegen - GET");

                if (model != null)
                {
                    if (BenutzerVerwaltung.LegeMitarbeiterAn(model.Fa_id, model.Emailadresse, model.Vorname, model.Nachname, model.NeuesPasswort, Innovation4Austria.web.AppCode.Validation.FIRMENANSPRECHPARTNER))
                    {
                        TempData[ConstStrings.SUCCESS_MESSAGE] = Validierungen.SpeichernErfolgreich;
                    }
                    else
                    {
                        TempData[ConstStrings.ERROR_MESSAGE] = Validierungen.SpeichernFehlgeschlagen;
                    }
                }
            }
            return RedirectToAction("FirmenAuflistung");
        }

        /// <summary>
        /// Aufruf der View zum Anlegen einer Firma
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult FirmaAnlegen()
        {
            return View();
        }

        /// <summary>
        /// Die Daten der neuen Firma werden entgegengenommen und an die BL weitergeleitet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult FirmaAnlegen(FirmaAnlegenModel model)
        {
            bool FirmaAnlegen = false;
            log.Info("Innovation4AustriaController - FirmaAnlegen - Post");
            if (ModelState.IsValid)
            {

                if (FirmaAnlegen = FirmenVerwaltung.FirmaAnlegen(model.Bezeichnung, model.Strasse, model.Nummer, model.Plz, model.Ort))
                {
                    TempData[ConstStrings.SUCCESS_MESSAGE] = Validierungen.FirmaAnlegegSuccess;
                }
                else
                {
                    TempData[ConstStrings.ERROR_MESSAGE] = Validierungen.FirmaAnlegenError;
                }
            }
            return RedirectToAction("FirmenAuflistung", "Innovation4AustriaMitarbeiter");
        }

        /// <summary>
        /// Aufruf aller Rechnungen wobei unterschieden wird, zwischen "schon bezahlt", "noch nicht abrechenbar" und "abrechenbar" 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult RechnungsUebersicht()
        {
            log.Info("Innovation4Austria - RechnungsUebersicht - GET");


            List<RechnungsuebersichtModel> alleRechnungen = new List<RechnungsuebersichtModel>();
            for (int i = 1; i <= 12; i++)
            {

                RechnungsuebersichtModel Monatsabrechnung = new RechnungsuebersichtModel();
                Monatsabrechnung.Monat = i;
                Monatsabrechnung.Jahr = DateTime.Now.AddYears(-1).Year;
                List<Buchungsdetails> CountBuchungen = RechnungsVerwaltung.BuchungenFuerMonatVorhanden(Monatsabrechnung.Monat, Monatsabrechnung.Jahr);
                if (CountBuchungen.Count > 0)
                {
                    Monatsabrechnung.schonBezahlt = RechnungsVerwaltung.RechnungenFuerMonatVorhanden(Monatsabrechnung.Monat, Monatsabrechnung.Jahr);
                    if (Monatsabrechnung.schonBezahlt)
                    {
                        Monatsabrechnung.Bezeichnung = Innovation4Austria.web.AppCode.ConstStrings.RECHNUNG_SCHON_VORHANDEN;
                    }
                    else
                    {
                        Monatsabrechnung.Bezeichnung = Innovation4Austria.web.AppCode.ConstStrings.RECHNUNGEN_JETZT_ERZEUGEN;
                    }

                }
                else
                {
                    Monatsabrechnung.Bezeichnung = Innovation4Austria.web.AppCode.ConstStrings.KEINE_RECHNUNGEN_VORHANDEN;
                    Monatsabrechnung.schonBezahlt = true;
                }
                alleRechnungen.Add(Monatsabrechnung);
                alleRechnungen = alleRechnungen.OrderBy(x => x.Monat).ToList();
            }
            for (int i = 1; i <= 12; i++)
            {
                RechnungsuebersichtModel Monatsabrechnung = new RechnungsuebersichtModel();
                Monatsabrechnung.Monat = i;
                Monatsabrechnung.Jahr = DateTime.Now.Year;
                List<Buchungsdetails> CountBuchungen = RechnungsVerwaltung.BuchungenFuerMonatVorhanden(Monatsabrechnung.Monat, Monatsabrechnung.Jahr);
                if (CountBuchungen.Count > 0)
                {
                    Monatsabrechnung.schonBezahlt = RechnungsVerwaltung.RechnungenFuerMonatVorhanden(Monatsabrechnung.Monat, Monatsabrechnung.Jahr);
                    if (Monatsabrechnung.schonBezahlt)
                    {
                        Monatsabrechnung.Bezeichnung = Innovation4Austria.web.AppCode.ConstStrings.RECHNUNG_SCHON_VORHANDEN;
                    }
                    else
                    {
                        Monatsabrechnung.Bezeichnung = Innovation4Austria.web.AppCode.ConstStrings.RECHNUNGEN_JETZT_ERZEUGEN;
                    }

                }
                else
                {
                    Monatsabrechnung.Bezeichnung = Innovation4Austria.web.AppCode.ConstStrings.KEINE_RECHNUNGEN_VORHANDEN;
                    Monatsabrechnung.schonBezahlt = true;
                }
                if (i>=DateTime.Now.Month)
                {
                    Monatsabrechnung.Bezeichnung = Innovation4Austria.web.AppCode.ConstStrings.RECHNUNG_KANN_NICHT_ERZEUGT_WERDEN;
                    Monatsabrechnung.schonBezahlt = true;
                }
                alleRechnungen.Add(Monatsabrechnung);
            }
            return View(alleRechnungen);
        }

        /// <summary>
        /// Alle Rechnungen eines Monats und eines Jahres werden erzeugt
        /// </summary>
        /// <param name="Monat"></param>
        /// <param name="Jahr"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult RechnungenFuerMonatProduzieren(int Monat, int Jahr)
        {
            log.Info("Innovation4AustriaMitarbeiterController - RechnungenFuerMonatProduzieren - Post");
            RechnungsVerwaltung.ErstelleRechnungenFuerMonat(Monat, Jahr);
            return RedirectToAction("RechnungsUebersicht");
        }


    }
}