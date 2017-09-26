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
    public class BuchungController : BasisController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Authorize]
        [HttpPost]
        public ActionResult BuchungsDetails(DateTime StartDatum, DateTime EndDatum, string datumVonBis, int Id)
        {
            log.Info("Buchung - BuchungsDetails - Get");


            BuchungsDetailModel buchungsDetails = new BuchungsDetailModel();
            buchungsDetails = AutoMapper.Mapper.Map<BuchungsDetailModel>(RaumVerwaltung.GesuchterRaum(Id));
            buchungsDetails.RaumAusstattung = new List<RaumAusstattungsFilterModel>();

            List<Raum_Ausstattung> raumAusstattung = RaumVerwaltung.RaumAusstattungEinesRaumes(Id);
            buchungsDetails.RaumAusstattung = new List<RaumAusstattungsFilterModel>();

            foreach (var item in raumAusstattung)
            {
                RaumAusstattungsFilterModel model = new RaumAusstattungsFilterModel()
                {
                    Bezeichnung = item.Ausstattung.Bezeichnung,
                    Ausstattungs_Id = item.Ausstattungs_Id
                };
                buchungsDetails.RaumAusstattung.Add(model);
            }

            //buchungsDetails.RaumAusstattung = AutoMapper.Mapper.Map<List<RaumAusstattungsFilterModel>>(RaumVerwaltung.RaumAusstattungEinesRaumes(Id));

            List<FirmenAusWahlModel> alleFirmen = AutoMapper.Mapper.Map<List<FirmenAusWahlModel>>(FirmenVerwaltung.LadeAlleFirmen());
            
            //buchungsDetails.RaumAusstattung = mapRaumAusstattung;
            buchungsDetails.Firma = alleFirmen;
            
            buchungsDetails.VonDatum = StartDatum;
            buchungsDetails.BisDatum = EndDatum;

            return View(buchungsDetails);
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult Raumbuchung(int Id, DateTime VonDatum, DateTime BisDatum, decimal Preis, int? Fa_Id)
        {
            bool gebucht = false;
            log.Info("BuchnungController - Raumbuchung - POST");
            Benutzer aktBenutzer = BenutzerAdministrator.GetUser(User.Identity.Name);
            int fa_id = 0;
            if (Fa_Id == null)
            {                
                fa_id = (int)aktBenutzer.Firma_id;
            }
            if (BuchungsVerwaltung.KontrolliereObBuchungMoeglich(aktBenutzer.Id))
            {
                TempData[ConstStrings.ERROR_MESSAGE] = Validierungen.BuchenFehlgeschlagen;
                return RedirectToAction("Dashboard", "Benutzer");
            }

            VerbindlichBuchenModel verbindlichBuchenModel = new VerbindlichBuchenModel();
            
            if (Id >= 0)
            {
                verbindlichBuchenModel.buchung_id = BuchungsVerwaltung.ErstelleBuchung(Id,fa_id);
            }

            if (verbindlichBuchenModel.buchung_id >=0)
            {
                if (gebucht = BuchungsVerwaltung.ErstelleBuchungDetails(verbindlichBuchenModel.buchung_id, VonDatum, BisDatum, Preis))
                {
                    TempData[ConstStrings.SUCCESS_MESSAGE] = Validierungen.BuchenErfolgreich;
                }
                else
                {
                    TempData[ConstStrings.ERROR_MESSAGE] = Validierungen.BuchenFehlgeschlagen;
                }
                
            }

            return RedirectToAction("Laden", "Raum");
        }

        [Authorize]
        [HttpGet]
        public  ActionResult Stornieren(int Id)
        {
            Benutzer aktBenutzer = BenutzerAdministrator.GetUser(User.Identity.Name);

            Buchung aktBuchung = BuchungsVerwaltung.HoleBuchung(Id);
            List<Buchungsdetails> BuchungsDetailsZuBuchung = BuchungsVerwaltung.BuchungsDetailsVonBuchung(aktBuchung.Id);
            BuchungsDetailsZuBuchung = BuchungsDetailsZuBuchung.OrderBy(x => x.Datum).ToList();
            foreach (var item in BuchungsDetailsZuBuchung)
            {
                if (item.Datum<DateTime.Now.AddDays(1))
                {
                    KreditkartenModel StornoModel = new KreditkartenModel();
                    StornoModel.KreditkartenBezeichnung = new List<KreditkartenArtModel>();
                    List<Kreditkarte> alleKreditkarten = RechnungsVerwaltung.AlleKreditKarten();
                    foreach (var kreditkarte in alleKreditkarten)
                    {
                        KreditkartenArtModel model = new KreditkartenArtModel()
                        {
                            bezeichnung = kreditkarte.bezeichnung,
                            id = kreditkarte.id
                        };
                        StornoModel.Buchung_id = Id;
                        StornoModel.KreditkartenBezeichnung.Add(model);
                        StornoModel.Rechnungsbetrag = BuchungsDetailsZuBuchung.Sum(x => x.Preis)/2;
                        
                    }
                    BuchungsVerwaltung.SperreVonUser(aktBenutzer.Id);
                    return View(StornoModel);
                }
            }


            log.Info("BuchungController - Stornieren -Get");
            if (Id>0)
            {
               bool storniert =  BuchungsVerwaltung.Stornieren(Id);
            }
            return RedirectToAction("Dashboard", "Benutzer");
        }


    }
}