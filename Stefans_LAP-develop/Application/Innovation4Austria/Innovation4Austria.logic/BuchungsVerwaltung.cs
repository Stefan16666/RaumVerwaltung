using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Innovation4Austria.logic
{
    public class BuchungsVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// retourniert alle Räume die gebucht sind. Stornierte Räume (!Aktiv) werden nicht zurückgegen
        /// </summary>
        /// <param name="fa_id"></param>
        /// <returns></returns>
        public static List<Buchung> GebuchteRaeume(int fa_id)
        {
            log.Info("RaumVerwaltung - BookedRooms");
            List<Buchung> bookedRooms = new List<Buchung>();
            if (fa_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        bookedRooms = context.AlleBuchungen.Where(x => x.Firma_id == fa_id && x.Aktiv == true).ToList();

                        if (bookedRooms == null)
                        {
                            log.Info("RaumVerWaltung - BookedRooms - There are no booked rooms found");
                        }

                    }
                }
                catch (Exception ex)
                {
                    log.Error("RaumVerwaltung - BookedRooms - Connection to database failed", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return bookedRooms;
        }

        public static int AnzahlTage(int id)
        {
            log.Info("RaumVerwaltung - AnzahlTage");
            Raum neuerRaum = new Raum();
            int AnzahlTage = 0;
            if (id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        List<Buchung> alleBuchungen = context.AlleBuchungen.Where(x => x.Raum_id == id).ToList();
                        foreach (var buchung in alleBuchungen)
                        {
                            List<Buchungsdetails> alleDeatailsEinerBuchung = context.AlleBuchungsdetails.Where(x => x.Buchung_id == buchung.Id).ToList();
                            AnzahlTage = AnzahlTage + alleDeatailsEinerBuchung.Count;
                        }

                    }
                }
                catch (Exception ex)
                {
                    log.Error("RaumVerwaltung - AnzahlTage - Connection to database failed", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return AnzahlTage;
        }

        /// <summary>
        /// Gibt alle Buchungen zu einem bestimmten Monat zurück
        /// </summary>
        /// <param name="monatnummer"></param>
        /// <returns></returns>
        public static List<Buchungsdetails> BuchungsDetailsVonMonat(int monatnummer)
        {
            log.Info("BuchungsVerwaltung - BuchungsDetailVonMonat");

            List<Buchungsdetails> alleBuchungsDetailsvonEinemMonat = new List<Buchungsdetails>();

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    alleBuchungsDetailsvonEinemMonat = context.AlleBuchungsdetails.Where(x => x.Datum.Month == monatnummer).ToList();

                }
            }
            catch (Exception ex)
            {
                log.Error("BuchungsVerwaltung - BuchungsDetailsVonMonat - DB-Verbindung fehlgeschlagen", ex);
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return alleBuchungsDetailsvonEinemMonat;
        }

        public static bool KontrolliereObBuchungMoeglich(int id)
        {
            log.Info("BuchungsVerwaltung - KontrolliereObBuchungMoeglich");
            bool moeglich = true;
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    Stornierung stornomodel = context.Stornierung.Where(x => x.Benutzer_id == id).FirstOrDefault();
                    if (stornomodel != null)
                    {
                        if (DateTime.Now < stornomodel.Datum.AddDays(3))
                        {
                            moeglich = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("BuchungsVerwaltung - KontrolliereObBuchungMoeglich - DB-Verbindung fehlgeschlagen", ex);
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return moeglich;
        }

        public static Buchung HoleBuchung(int id)
        {
            Buchung gesBuchung = new Buchung();

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    gesBuchung = context.AlleBuchungen.Where(x => x.Id == id).Include(y => y.AlleBuchungsdetails).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return gesBuchung;
        }

        /// <summary>
        /// reourniere das Anfangs- oder Enddatum einer Buchung auswählbar über bool "vonDatum"
        /// </summary>
        /// <param name="buchung_id"></param>
        /// <param name="vonDatum"></param>
        /// <returns></returns>
        public static DateTime datum(int buchungsDetail_id, bool vonDatum)
        {
            List<Buchungsdetails> buchungsdetailsEinerBuchung = null;
            DateTime date = new DateTime();
            Console.Write("BuchungsVerwaltung - datum");
            if (buchungsDetail_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        buchungsdetailsEinerBuchung = context.AlleBuchungsdetails.Where(x => x.Buchung_id == buchungsDetail_id).ToList();
                        if (vonDatum)
                        {
                            date = (from x in buchungsdetailsEinerBuchung orderby x.Datum select x.Datum).FirstOrDefault();
                        }
                        else
                        {
                            date = (from x in buchungsdetailsEinerBuchung orderby x.Datum descending select x.Datum).FirstOrDefault();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("BuchungsVerwaltung - Buchung - es konnte keine Datenbankverbindung hergestellt werden", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return date;
        }

        public static void SperreVonUser(int id)
        {
            log.Info("BuchungsVerwaltung - SperreVonUser");
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    Stornierung neueStornierung = new Stornierung();
                    neueStornierung.Benutzer_id = id;
                    neueStornierung.Datum = DateTime.Now;
                    context.Stornierung.Add(neueStornierung);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("BuchungsVerwaltung - Buchung - es konnte keine Datenbankverbindung hergestellt werden", ex);
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
        }

        /// <summary>
        /// Setzt das Aktiv bit einer Buchung auf false und storniert sie so
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Stornieren(int id)
        {
            log.Info("BuchungsVerwaltung - Stornieren");
            bool storniert = false;
            //Buchung gesBuchung = new Buchung();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    Buchung gesBuchung = context.AlleBuchungen.Where(x => x.Id == id).FirstOrDefault();

                    gesBuchung.Aktiv = false;
                    context.SaveChanges();
                    storniert = true;

                }
            }
            catch (Exception ex)
            {
                log.Error("BuchungsVerwaltung - Buchung - es konnte keine Datenbankverbindung hergestellt werden", ex);
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return storniert;
        }

        /// <summary>
        /// trägt eine Buchungsdetails ein, jedes Datum vom Anfang-Enddatum wird tageweise in einen Buchungssatz
        /// </summary>
        /// <param name="buchung_id"></param>
        /// <param name="vonDatum"></param>
        /// <param name="BisDatum"></param>
        /// <param name="preis"></param>
        /// <returns></returns>
        public static bool ErstelleBuchungDetails(int buchung_id, DateTime vonDatum, DateTime BisDatum, decimal preis)
        {
            bool erfolgreich = false;
            log.Info("BuchungsVerwaltung - ErstelleBuchungDetails");
            if (buchung_id > 0)
            {
                if (vonDatum != null)
                {
                    if (BisDatum != null)
                    {
                        int wievieleTage = (BisDatum - vonDatum).Days;
                        wievieleTage++;
                        try
                        {
                            using (var context = new Innovation4AustriaEntities())
                            {
                                for (int i = 0; i < wievieleTage; i++)
                                {
                                    Buchungsdetails neueBuchung = new Buchungsdetails()
                                    {
                                        Buchung_id = buchung_id,
                                        Datum = vonDatum,
                                        Preis = preis
                                    };
                                    context.AlleBuchungsdetails.Add(neueBuchung);
                                    if (vonDatum < BisDatum)
                                    {
                                        vonDatum = vonDatum.AddDays(1);
                                    }
                                }
                                if (context.SaveChanges() == wievieleTage)
                                {
                                    erfolgreich = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("BuchungsVerwaltung - ErstelleBuchungDetals - fehlgeschlagen");
                            if (ex.InnerException != null)
                            {
                                log.Info(ex.InnerException);
                            }
                        }

                    }
                }
            }
            return erfolgreich;

        }

        /// <summary>
        /// gibt alle Buchugnsdetails einer Buchung zurück
        /// </summary>
        /// <param name="buchung_id"></param>
        /// <returns></returns>
        public static List<Buchungsdetails> BuchungsDetailsVonBuchung(int buchung_id)
        {
            log.Info("RaumVerwaltung - BuchungsDetailsVonBuchung");
            List<Buchungsdetails> detailVonBuchung = null;
            if (buchung_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {

                        detailVonBuchung = context.AlleBuchungsdetails.Include(x => x.Rechnungsdetails).Where(x => x.Buchung_id == buchung_id).ToList();
                        if (detailVonBuchung == null)
                        {
                            log.Warn("BuchungsVerwaltung - BuchungsDetailsVonBuchung - keine Details zur Buchung gefunden");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("BuchungsVerwaltung - BuchungsDetailsVonBuchung - Datenbankverbindung fehlgeschlagen", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return detailVonBuchung;
        }


        /// <summary>
        /// Erstellt eine Buchung und retouniert die ID
        /// </summary>
        /// <param name="raum_id"></param>
        /// <param name="firma_id"></param>
        /// <returns></returns>
        public static int ErstelleBuchung(int raum_id, int firma_id)
        {
            Buchung neueBuchung = new Buchung();
            neueBuchung.Raum_id = raum_id;
            neueBuchung.Firma_id = firma_id;
            neueBuchung.Aktiv = true;
            log.Info("BuchungsVerwaltung - ErstelleBuchung");
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    context.AlleBuchungen.Add(neueBuchung);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("BuchungsVerwaltung - BuchungsDetailsVonBuchung - Datenbankverbindung fehlgeschlagen", ex);
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }

            return neueBuchung.Id;
        }


    }
}

