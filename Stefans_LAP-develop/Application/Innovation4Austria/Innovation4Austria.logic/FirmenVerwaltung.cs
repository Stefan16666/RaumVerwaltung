using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using log4net;

namespace Innovation4Austria.logic
{
    public class FirmenVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Ladet alle Firmen 
        /// </summary>
        /// <returns></returns>
        public static List<Firma> LadeAlleFirmen()
        {
            log.Info("FirmenVerwaltung -  LadeAlleFirmen");

            List<Firma> alleFirmen = null;
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    alleFirmen = context.AlleFirmen.Where(x => x.aktiv).ToList();
                    if (alleFirmen == null)
                    {
                        log.Warn("FirmenVerwaltung - LadeAlleFirmen - keine Firmen gefunden");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("FirmenVerwlatung - ladeAlleFirmen - DatenVerbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return alleFirmen;
        }

        /// <summary>
        /// Retouniert eine Firma anhand der ID
        /// </summary>
        /// <param name="firma_id"></param>
        /// <returns></returns>
        public static Firma LiefereFirma(int firma_id)
        {
            log.Info("FirmenVerwaltung - LiefereFirma");
            Firma firma = new Firma();
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    firma = context.AlleFirmen.Where(x => x.Id == firma_id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error("FirmenVerwlatung - LiefereFirma - DatenVerbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return firma;
        }

        /// <summary>
        /// Aktualisiert die Daten von einer Firma
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bezeichnung"></param>
        /// <param name="aktiv"></param>
        /// <param name="nummer"></param>
        /// <param name="ort"></param>
        /// <param name="plz"></param>
        /// <param name="strasse"></param>
        /// <returns>erfolgreich/ nach Aktualisierung soll die Bestätigung erfolgen</returns>
        public static bool FirmaAktualisierung(int id, string bezeichnung, bool aktiv, string nummer, string ort, string plz, string strasse)
        {
            bool erfolgreich = false;
            log.Info("FirmenVerwaltung - FirmaAktualisierung");
            Firma aktFirma = null;

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    aktFirma = context.AlleFirmen.Where(x => x.Id == id).FirstOrDefault();
                    if (aktiv)
                    {
                        aktFirma.Bezeichnung = bezeichnung;
                        aktFirma.Nummer = nummer;
                        aktFirma.Ort = ort;
                        aktFirma.Plz = plz;
                        aktFirma.Strasse = strasse;

                    }
                    else
                    {
                        aktFirma.aktiv = aktiv;
                    }
                    int result = context.SaveChanges();
                    if (result > 0)
                    {
                        erfolgreich = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("FirmenVerwaltung - FirmaAktualisierung - DbVerbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }

            }
            return erfolgreich;
        }

        /// <summary>
        /// legt eine neue Firma an
        /// </summary>
        /// <param name="bezeichnung"></param>
        /// <param name="strasse"></param>
        /// <param name="nummer"></param>
        /// <param name="plz"></param>
        /// <param name="ort"></param>
        /// <returns>betäigt diese wenn erfolgreich</returns>
        public static bool FirmaAnlegen(string bezeichnung, string strasse, string nummer, string plz, string ort)
        {
            log.Info("FirmenVerwaltung - FirmaAnlegen");
            Firma neueFirma = new Firma()
            {
                Bezeichnung = bezeichnung,
                Strasse = strasse,
                Nummer = nummer,
                Plz = plz,
                Ort = ort,
                aktiv= true
            };

            bool erfolgreich = false;
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    context.AlleFirmen.Add(neueFirma);
                    if (context.SaveChanges() > 0)
                    {
                        erfolgreich = true;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("FirmenVerwaltung - FirmaAktualisierung - DbVerbindung fehlgeschlagen");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }

            }
            return erfolgreich;
        }
    }
}
