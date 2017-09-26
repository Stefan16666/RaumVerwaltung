using Innovation4Austria.logic;
using Innovation4Austria.web.AppCode;
using Innovation4Austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovation4Austria.web.Controllers
{
    public class RechnungController : BasisController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Authorize]
        [HttpGet]
        public ActionResult Erstellen(int Monatnummer)
        {
            log.Info("Rechnung - Erstellen - GET");

            Benutzer aktBenutzer = BenutzerAdministrator.GetUser(User.Identity.Name);

            List<Buchungsdetails> gesuchteBuchungsDetails = RechnungsVerwaltung.MonatsBuchungsDetails((int)aktBenutzer.Firma_id, Monatnummer);

            Rechnung rechnung = RechnungsVerwaltung.LiefereRechnungzuFirmaAusMonat(gesuchteBuchungsDetails);

            RechnungErstellenModel rechnungsModel = new RechnungErstellenModel();

            Firma firma = FirmenVerwaltung.LiefereFirma((int)aktBenutzer.Firma_id);

            rechnungsModel.Rg_Id = rechnung.Id;
            rechnungsModel.Firmenname = firma.Bezeichnung;
            rechnungsModel.FirmenstrassenNummer = firma.Nummer;
            rechnungsModel.FirmenOrt = firma.Ort;
            rechnungsModel.Firmenstrasse = firma.Strasse;
            rechnungsModel.FirmenPlz = firma.Plz;
            rechnungsModel.Datum = (DateTime)rechnung.Datum;
            rechnungsModel.Firmenname = firma.Bezeichnung;
            rechnungsModel.Gesamtpreis = gesuchteBuchungsDetails.Sum(x => x.Preis);
            rechnungsModel.Steuerbetrag = (rechnungsModel.Gesamtpreis / 100) * 20;
            rechnungsModel.RechnungsDetails = new List<RechnungsDetailModel>();
            foreach (var buchungsDetail in gesuchteBuchungsDetails)
            {
                RechnungsDetailModel model = new RechnungsDetailModel()
                {
                    Buchungs_ID = buchungsDetail.Id.ToString(),
                    Buchungsdatum = buchungsDetail.Datum,
                    Preis = buchungsDetail.Preis,
                    RaumNummer = RaumVerwaltung.GesuchterRaumName(buchungsDetail.Buchung_id)
                };

                rechnungsModel.RechnungsDetails.Add(model);
            }
            DateTime VonDatum = (from x in gesuchteBuchungsDetails orderby x.Datum select x.Datum).FirstOrDefault();
            DateTime BisDatum = (from x in gesuchteBuchungsDetails orderby x.Datum descending select x.Datum).FirstOrDefault();

            return new Rotativa.ViewAsPdf("Erstellen", rechnungsModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Bezahlen(int Monatnummer)
        {
            log.Info("Rechnung - Bezahlen - GET");

            Benutzer aktBenutzer = BenutzerAdministrator.GetUser(User.Identity.Name);

            List<Buchungsdetails> gesuchteBuchungsDetails = RechnungsVerwaltung.MonatsBuchungsDetails((int)aktBenutzer.Firma_id, Monatnummer);
            KreditkartenModel KreditkartenVorlage = new KreditkartenModel();
            foreach (var item in gesuchteBuchungsDetails)
            {
                if (KreditkartenVorlage.Rechnungsnummer==0)
                {
                    Rechnungsdetails rgDeatail = RechnungsVerwaltung.RGDetailZuBuchung(item.Id);
                    KreditkartenVorlage.Rechnungsnummer = rgDeatail.Rechnung_Id;
                }               

            }  

            KreditkartenVorlage.KreditkartenBezeichnung = new List<KreditkartenArtModel>();
            List<Kreditkarte> alleKreditkarten = RechnungsVerwaltung.AlleKreditKarten();
            foreach (var kreditkarte in alleKreditkarten)
            {
                KreditkartenArtModel model = new KreditkartenArtModel()
                {
                    bezeichnung = kreditkarte.bezeichnung,
                    id = kreditkarte.id
                };
                KreditkartenVorlage.KreditkartenBezeichnung.Add(model);
            }

           KreditkartenVorlage.Rechnungsbetrag= gesuchteBuchungsDetails.Sum(x => x.Preis);

           return View(KreditkartenVorlage);
        }

        [HttpPost]
        public ActionResult Bezahlen(KreditkartenModel model)
        {
            log.Info("Rechnung - Bezahlen - POST");
            Benutzer aktBenutzer = BenutzerAdministrator.GetUser(User.Identity.Name);

            if (ModelState.IsValid)
            {
                if (model.GültigBisJahr>=DateTime.Now.Year)
                {
                    if (model.GültigBisMonat>= DateTime.Now.Month)
                    {
                        if (checkLuhn(model.KreditKartenNummer))
                        {
                            RechnungsVerwaltung.RechnungIstBezahlt(model.Rechnungsnummer);
                        }
                        else
                        {
                            TempData[ConstStrings.SUCCESS_MESSAGE] = Validierungen.WurdeSchonBezahlt;
                        }
                    }
                }
            }


            
            return RedirectToAction("Dashboard", "Benutzer");
        }

        [HttpPost]
        public ActionResult Stornieren(KreditkartenModel model)
        {
            log.Info("Rechnung - Bezahlen - POST");
            Benutzer aktBenutzer = BenutzerAdministrator.GetUser(User.Identity.Name);

            if (ModelState.IsValid)
            {
                if (model.GültigBisJahr >= DateTime.Now.Year && model.GültigBisMonat >= DateTime.Now.Month)
                {
                    if (checkLuhn(model.KreditKartenNummer))
                    {
                        BuchungsVerwaltung.Stornieren(model.Buchung_id);
                        TempData[ConstStrings.SUCCESS_MESSAGE] = Validierungen.StornoErfolreich;
                    }
                    else
                    {
                        TempData[ConstStrings.ERROR_MESSAGE] = Validierungen.BezahlungNichtMoeglich;
                    }
                }
                else
                {
                    TempData[ConstStrings.ERROR_MESSAGE] = Validierungen.BezahlungNichtMoeglich;                    
                }
                
            }
            return RedirectToAction("Dashboard", "Benutzer");
        }

        public static bool checkLuhn(string creditCardNumber)
        {
          
            if (string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }

            int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((e) => e / 10 + e % 10);
      
            return sumOfDigits % 10 == 0;
        }
    }
}
