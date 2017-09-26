using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Innovation4Austria.logic
{
    public class RechnungsVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// ein Rechnungsdetail einer Firma wird herausgesucht 
        /// </summary>
        /// <param name="buchungsDetail_id"></param>
        /// <returns></returns>
        public static Rechnungsdetails EinRechnungsDetailsEinesBuchungsDetails(int buchungsDetail_id)
        {
            Rechnungsdetails detail = null;
            log.Info("RechnungsVerwaltung - AlleRechnungsDetailsEinerBuchung");
            if (buchungsDetail_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {

                        {
                            detail = context.AlleRechnungsdetails.Where(x => x.Buchungsdetail_Id == buchungsDetail_id).FirstOrDefault();
                            if (detail == null)
                            {
                                log.Warn("RechnungsVerwaltung - AlleRechnungsDetailsEinerBuchung - es wurden keine Rechnungsdetails zu der Buchung gefunden");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("RechnungsVerwaltung - AlleRechnungsDetailsEinerBuchung - es konnte keine Datenbankverbindung hergestellt werden", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return detail;
        }

        /// <summary>
        /// Sucht alle Buchungsdetails einer Firma aus einem bestimmten Monat
        /// </summary>
        /// <param name="fa_id"></param>
        /// <param name="monat"></param>
        /// <returns></returns>
        public static List<Buchungsdetails> MonatsBuchungsDetails(int fa_id, int monat)
        {
            log.Info("RechnungsVerwaltung - MonatsRechnung");
            List<Rechnungsdetails> gesRGdetails = new List<Rechnungsdetails>();
            List<Buchungsdetails> gesuchteBuchungsDetails = new List<Buchungsdetails>();
            List<Rechnung> monatsRechnungen = new List<Rechnung>();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {

                    monatsRechnungen = context.AlleRechnungen.Where(x => x.fa_id == fa_id).ToList();
                    foreach (var eineRechnung in monatsRechnungen)
                    {
                        List<Rechnungsdetails> alleRGDetails = context.AlleRechnungsdetails.Where(x => x.Rechnung_Id == eineRechnung.Id).ToList();
                        foreach (var rechnungsdetail in alleRGDetails)
                        {
                            Buchungsdetails einBuchungsDetail = context.AlleBuchungsdetails.Where(x => x.Id == rechnungsdetail.Buchungsdetail_Id && x.Datum.Month == monat).Include(y => y.Rechnungsdetails).FirstOrDefault();
                            if (einBuchungsDetail != null)
                            {
                                gesuchteBuchungsDetails.Add(einBuchungsDetail);
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {

                log.Error("RechnungsVerwaltung - MonatsBuchungsDetails - DB-Verbindung fehlgeschlagen");
            }
            return gesuchteBuchungsDetails;
        }

        public static Rechnungsdetails RGDetailZuBuchung(int id)
        {

            Rechnungsdetails RGDeatailZuBuchungsdeatil = new Rechnungsdetails();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    RGDeatailZuBuchungsdeatil = context.AlleRechnungsdetails.Where(x => x.Buchungsdetail_Id == id).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RGDeatailZuBuchungsdeatil;
        }

        public static List<Kreditkarte> AlleKreditKarten()
        {
            log.Info("RechnungsVerwaltung - AlleKreditKarten");

            List<Kreditkarte> alleKreditkarten = new List<Kreditkarte>();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    alleKreditkarten = context.Kreditkarte.ToList();
                }
            }
            catch (Exception ex)
            {

                log.Error("RechnungsVerwaltung - MonatsBuchungsDetails - DB-Verbindung fehlgeschlagen");
            }
            return alleKreditkarten;
        }

        public static bool RechnungIstBezahlt(int rechnungsnummer)
        {
            log.Info("RechnungsVerwaltung - Rechnung bezahlen");
            bool bezahlt = false;
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    Rechnung bezahlteRechnung = context.AlleRechnungen.Where(x => x.Id == rechnungsnummer).FirstOrDefault();
                    if (!bezahlteRechnung.bezahlt)
                    {
                        bezahlteRechnung.bezahlt = true;
                        bezahlt = true;
                    }
                }
            }

            catch (Exception ex)
            {

                log.Error("RechnungsVerwaltung - RechnungIstBezahlt - DB-Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return bezahlt;
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="BgDetails"></param>
        /// <returns></returns>
        public static Rechnung LiefereRechnungzuFirmaAusMonat(List<Buchungsdetails> BgDetails)
        {
            log.Info("Rechnungsverwaltung - LiefereRechnungzuFirmaAusMonat");
            Rechnung rechnung = new Rechnung();
            List<Rechnungsdetails> RgDetails = new List<Rechnungsdetails>();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    foreach (var buchungsdetail in BgDetails)
                    {
                        Rechnungsdetails RgDetail = context.AlleRechnungsdetails.Where(x => x.Buchungsdetail_Id == buchungsdetail.Id).FirstOrDefault();
                        if (rechnung != null)
                        {
                            rechnung = context.AlleRechnungen.Where(x => x.Id == RgDetail.Rechnung_Id).FirstOrDefault();
                        }
                        RgDetails.Add(RgDetail);
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return rechnung;

        }
        public static List<Rechnung> RechnungenEinerFirma(int firma_id)
        {
            log.Info("RechnugnsVerwaltung - RechnungenEinerFirma");

            List<Rechnung> rechnungen = new List<Rechnung>();

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    rechnungen = context.AlleRechnungen.Where(x => x.fa_id == firma_id).ToList();
                }

            }
            catch (Exception ex)
            {
                log.Error("RechnugnsVerwaltung - RechnungenEinerFirma - DB_Verbindung fehlgeschlagen");
            }
            return rechnungen;
        }

        public static List<Rechnungsdetails> RechnungsDetailsEinerRechnung(int id)
        {
            log.Info("RechnugnsVerwaltung - RechnungsDetailsEinerRechnung");

            List<Rechnungsdetails> RgDetails = new List<Rechnungsdetails>();

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    RgDetails = context.AlleRechnungsdetails.Where(x => x.Rechnung_Id == id).ToList();
                }

            }
            catch (Exception ex)
            {
                log.Error("RechnugnsVerwaltung - RechnungsDetailsEinerRechnung - DB_Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return RgDetails;
        }

        public static Buchungsdetails BuchungsDetailEinerRechnung(int id)
        {
            log.Info("RechnungsVerwaltung - RechnungsDetailsEinerRechnung");

            Buchungsdetails buchungsDetail = new Buchungsdetails();

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    buchungsDetail = context.AlleBuchungsdetails.Where(x => x.Id == id).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                log.Error("RechnugnsVerwaltung - RechnungsDetailsEinerRechnung - DB_Verbindung fehlgeschlagen");
            }
            return buchungsDetail;
        }

        public static bool RechnungenFuerMonatVorhanden(int monat, int jahr)
        {
            log.Info("RechnungsVerwaltung - RechnungsDetailsEinerRechnung");
            List<Buchungsdetails> BuchungenZuMonat = new List<Buchungsdetails>();
            List<Rechnungsdetails> RechnungsDetailListe = new List<logic.Rechnungsdetails>();
            bool vorhanden = false;
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    BuchungenZuMonat = context.AlleBuchungsdetails.Where(x => x.Datum.Month == monat && x.Datum.Year == jahr).ToList();
                    RechnungsDetailListe = context.AlleRechnungsdetails.ToList();
                    BuchungenZuMonat = BuchungenZuMonat.Where(x => x.Rechnungsdetails.Count > 0).ToList();
                    if (BuchungenZuMonat.Count != 0)
                    {
                        vorhanden = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RechnugnsVerwaltung - RechnungsDetailsEinerRechnung - DB_Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return vorhanden;
        }

        public static List<Buchungsdetails> BuchungenFuerMonatVorhanden(int monat, int jahr)
        {
            log.Info("RechnungsVerwaltung - RechnungsDetailsEinerRechnung");
            List<Buchungsdetails> BuchungenZuMonat = new List<Buchungsdetails>();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    BuchungenZuMonat = context.AlleBuchungsdetails.Where(x => x.Datum.Month == monat && x.Datum.Year == jahr).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("RechnugnsVerwaltung - RechnungsDetailsEinerRechnung - DB_Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return BuchungenZuMonat;
        }

        public static void ErstelleRechnungenFuerMonat(int monat, int jahr)
        {
            log.Info("RechnungsVerwaltung - ErstelleRechnungenFuerMonat");
            List<Buchungsdetails> buchungsDetailsFuerMonat = new List<Buchungsdetails>();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    buchungsDetailsFuerMonat = context.AlleBuchungsdetails.Where(x => x.Datum.Year == jahr && x.Datum.Month == monat && x.Buchung.Aktiv).ToList();
                    foreach (var buchungsDetail in buchungsDetailsFuerMonat)
                    {
                        List<Buchungsdetails> buchungsDetailsZuFirma = new List<Buchungsdetails>();
                        buchungsDetailsZuFirma = buchungsDetailsFuerMonat.Where(x => x.Buchung.Firma_id == buchungsDetail.Buchung.Firma_id).ToList();
                        Rechnung neueRechnung = new Rechnung();
                        neueRechnung.Datum = DateTime.Now;
                        neueRechnung.fa_id = buchungsDetail.Buchung.Firma_id;
                        context.AlleRechnungen.Add(neueRechnung);
                        context.SaveChanges();
                        foreach (var buchungsDetaileinesMonats in buchungsDetailsZuFirma)
                        {
                            Rechnungsdetails DetailZuFirma = new Rechnungsdetails();
                            DetailZuFirma.Buchungsdetail_Id = buchungsDetaileinesMonats.Id;
                            DetailZuFirma.Rechnung_Id = neueRechnung.Id;
                            context.AlleRechnungsdetails.Add(DetailZuFirma);
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("RechnugnsVerwaltung - ErstelleRechnungenFuerMonat - DB_Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
        }
    }
}
