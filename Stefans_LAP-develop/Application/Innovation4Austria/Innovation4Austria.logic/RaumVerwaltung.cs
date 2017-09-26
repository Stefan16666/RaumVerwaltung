using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Innovation4Austria.logic
{
    public class RaumVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Sucht einen Raum anhand der ID
        /// </summary>
        /// <param name="raum_id"></param>
        /// <returns></returns>
        public static Raum GesuchterRaum(int raum_id)
        {
            log.Info("RaumVerwaltung - gesuchterRaum");
            Raum raum = null;
            if (raum_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        raum = context.AlleRaeume.Where(x => x.Id == raum_id).Include("Art").FirstOrDefault();
                        if (raum == null)
                        {
                            log.Warn("RaumVerwaltung - gesuchterRaum - kein Raum gefunden");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("RaumVerwaltung - gesuchterRaum - Datenbankverbindung fehlgeschlagen", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return raum;

        }


        /// <summary>
        /// Liefert alle Räume retour
        /// </summary>
        /// <returns></returns>
        public static List<Raum> alleRaeume()
        {
            log.Info("RaumVerwaltung - alleRaeume");

            List<Raum> mAlleRaeume = null;

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    mAlleRaeume = context.AlleRaeume.Include(x => x.Art).ToList();
                    if (mAlleRaeume == null)
                    {
                        log.Warn("Raeume wurden nicht gefunden");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RaumVerwltung - alleRaeume - Datenbankverbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }

            return mAlleRaeume;
        }

        /// <summary>
        /// gibt den Raumnamen einer Buchung retour
        /// </summary>
        /// <param name="buchung_id"></param>
        /// <returns></returns>
        public static string GesuchterRaumName(int buchung_id)
        {
            log.Info("RaumVerwaltung - GesuchterRaumName");

            string Raumname = "";
            Buchung gesBuchung = new Buchung();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    gesBuchung = context.AlleBuchungen.Where(x => x.Id == buchung_id).Include(y => y.Raum).FirstOrDefault();
                    Raumname = gesBuchung.Raum.Bezeichnung;

                }
            }
            catch (Exception ex)
            {
                log.Error("RaumVerwltung - alleRaeume - Datenbankverbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return Raumname;
        }

        /// <summary>
        /// liefert alle RaumArten
        /// </summary>
        /// <returns></returns>
        public static List<Art> AlleRaumArten()
        {

            log.Info("RaumVerwaltung - alleRaumArten");

            List<Art> alleRaumArten = null;

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    alleRaumArten = context.AlleArten.ToList();
                    if (alleRaumArten == null)
                    {
                        log.Warn("RaumVerwaltung - AlleRaumArten");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RaumVerwaltung - AlleRaumArten - Db-Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return alleRaumArten;
        }

        /// <summary>
        /// Liefert alle Raumausstattungen
        /// </summary>
        /// <returns></returns>
        public static List<Ausstattung> AlleRaumAusstattungen()
        {
            log.Info("RaumVerwaltung - AlleRaumAusstattungen");

            List<Ausstattung> alleAusstattungen = null;

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    alleAusstattungen = context.AlleAusstattungen.ToList();
                    if (alleAusstattungen == null)
                    {
                        log.Warn("RaumVerwaltung - AlleRaumAusstattungen - RaumAusstattungen auslesen fehlgeschlagen");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RaumVerwaltung - AlleRaumAusstattungen - DB - Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return alleAusstattungen;
        }

        /// <summary>
        /// Liefert alle RaumAusstattungen eines Raumes
        /// </summary>
        /// <param name="raum_id"></param>
        /// <returns></returns>
        public static List<Raum_Ausstattung> RaumAusstattungEinesRaumes(int raum_id)
        {
            log.Info("RaumVerwaltung - RaumAusstattungEinesRaumes");
            List<Raum_Ausstattung> raumAusstattungEinesRaumes = new List<Raum_Ausstattung>();

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    raumAusstattungEinesRaumes = context.AlleRaum_Ausstattungen.Include(y => y.Ausstattung).Where(x => x.Raum_id == raum_id).ToList();
                }
                if (raumAusstattungEinesRaumes == null)
                {
                    log.Warn("RaumVerwaltung - RaumAusstattungEinesRaumes - RaumAusstattungenEinesRaumes auslesen fehlgeschlagen");
                }

            }
            catch (Exception ex)
            {
                log.Error("RaumVerwaltung - RaumAusstattungEinesRaumes - DB - Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return raumAusstattungEinesRaumes;
        }

        /// <summary>
        /// Reourniert eine Liste von Räumen die in einem in einem Zeitraum frei sind mit der gesuchten Art und Ausstattung
        /// </summary>
        /// <param name="startdatum"></param>
        /// <param name="enddatum"></param>
        /// <param name="art_id"></param>
        /// <param name="ausstattung"></param>
        /// <returns></returns>
        public static List<Raum> GesuchteRaeume(DateTime startdatum, DateTime enddatum, int art_id, int[] ausstattung)
        {
            log.Info("RaumVerwaltung - GesuchteRaueme");

            List<Raum> raeume = new List<Raum>();
            List<Raum> gesuchteRaeume = new List<Raum>();
            List<Raum> temp = new List<Raum>();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {

                    List<Buchung> buchungen = new List<Buchung>();

                    List<Raum> belegteRaeume = new List<Raum>();

                    List<Buchungsdetails> buchungsdetails = context.AlleBuchungsdetails.Where(x => x.Datum >= startdatum && x.Datum <= enddatum).ToList();

                    raeume = context.AlleRaeume.Where(x => x.Art_id == art_id).Include(y => y.Art).ToList();

                    if (buchungsdetails != null)
                    {
                        foreach (var buchungsdetail in buchungsdetails)
                        {
                            Buchung buchung = context.AlleBuchungen.Where(x => x.Id == buchungsdetail.Buchung_id).FirstOrDefault();
                            buchungen.Add(buchung);
                        }
                    }

                    if (buchungen != null)
                    {
                        foreach (var buchung in buchungen)
                        {
                            Raum belegterRaum = context.AlleRaeume.Where(x => x.Id == buchung.Raum_id).FirstOrDefault();
                            belegteRaeume.Add(belegterRaum);
                        }
                    }

                    // hier wird nach der Art gefiltert, ist der Raum schon in den Zeitraum vergeben, wird er von den anzuzeigenden Räumen entfernt
                    if (belegteRaeume != null)
                    {
                        foreach (var belegterRaum in belegteRaeume)
                        {
                            raeume.RemoveAll(x => x.Id == belegterRaum.Id);
                        }
                    }
                    // hier wird nach der Ausstattung gefiltert. 
                    // wenn die Anzahl (in alle AlleRaum_Ausstattungen) Ausstattungs_Id gleich groß ist wie die in ausstattung[], dann soll es in die Liste aufgenommen werden
                    raeume = raeume.Where(x => x.AlleRaum_Ausstattungen.Select(y => y.Ausstattungs_Id)
                                          .Intersect(ausstattung).Count() == ausstattung.Count()).ToList();

                  

                }
            }
            catch (Exception ex)
            {
                log.Error("RaumVerwaltung - GesuchteRaeume - DB-Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return raeume;
        }

        /// <summary>
        /// Retouniert alle Räume 
        /// </summary>
        /// <returns></returns>
        public static List<Raum> GesuchteRaeume()
        {
            log.Info("RaumVerwaltung - GesuchteRaeume");

            List<Raum> gesuchteRaeume = new List<Raum>();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    gesuchteRaeume = context.AlleRaeume.Include(x => x.Art).Include(y => y.Bauwerk).ToList();
                    if (gesuchteRaeume == null)
                    {
                        log.Warn("RaumVerwaltung - gesuchteRaeume - keine Raeume gefunden");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RaumVerwaltung - GesuchteRaeume - db-Verbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return gesuchteRaeume;
        }
    }
}
