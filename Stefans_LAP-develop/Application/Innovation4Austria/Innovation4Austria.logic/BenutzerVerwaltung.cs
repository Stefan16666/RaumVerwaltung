using Innovation4Austria.logic;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Verwaltung
{
    public class BenutzerVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Sucht den Benutzer anhand der Emailadresse und dem Passwort heraus
        /// </summary>
        /// <param name="emailadresse"></param>
        /// <param name="passwort"></param>
        /// <returns></returns>
        public static bool Anmelden(string emailadresse, string passwort)
        {
            log.Info("BenutzerVerwaltung - Anmelden()");
            bool gefunden = false;
            Benutzer loginbenutzer = new Benutzer();
            if (emailadresse != null)
            {
                if (passwort != null)
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        try
                        {
                            loginbenutzer = context.AlleBenutzer.Include(x => x.Firma).Where(x => x.Emailadresse == emailadresse&& x.Aktiv==true).FirstOrDefault();
                            if (loginbenutzer != null)
                            {
                                if (loginbenutzer.Passwort.SequenceEqual(Tools.GenerierePasswort(passwort)))
                                {
                                    log.Info("BenutzerVerwaltung - Anmelden - Emailadresse und Passwort wurden gefunden");
                                    gefunden = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("BenutzerVerwaltung - Anmelden fehlgeschlagen ");
                            log.Info(ex.Message);
                        }
                    }
                }
            }
            return gefunden;
        }

        /// <summary>
        /// ersetzt das alte Passwort mit einem neuen
        /// </summary>
        /// <param name="emailadresse"></param>
        /// <param name="neuesPassword"></param>
        /// <param name="altesPasswort"></param>
        /// <returns></returns>
        public static BenutzerAdministrator.Passwortwechselergebnis Wechselpasswort(string emailadresse, string neuesPassword, string altesPasswort)
        {

            BenutzerAdministrator.Passwortwechselergebnis result = BenutzerAdministrator.Passwortwechselergebnis.UsernameInvalid;

            log.Info("WechselPasswort(emailadresse, neuesPassword, altesPasswort)");

            if (string.IsNullOrEmpty(emailadresse))
                throw new ArgumentNullException(nameof(emailadresse));
            else if (string.IsNullOrEmpty(neuesPassword))
                throw new ArgumentNullException(nameof(neuesPassword));
            else if (string.IsNullOrEmpty(altesPasswort))
                throw new ArgumentNullException(nameof(altesPasswort));
            else
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    try
                    {
                        Benutzer aktBenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == emailadresse).FirstOrDefault();

                        if (aktBenutzer == null)
                        {
                            result = BenutzerAdministrator.Passwortwechselergebnis.UsernameInvalid;
                        }
                        else if (!aktBenutzer.Aktiv == true)
                        {
                            result = BenutzerAdministrator.Passwortwechselergebnis.UserInactive;
                        }
                        else if (!aktBenutzer.Passwort.SequenceEqual(Tools.GenerierePasswort(altesPasswort)))
                        {
                            result = BenutzerAdministrator.Passwortwechselergebnis.PasswortInvalid;
                        }
                        else
                        {
                            log4net.LogicalThreadContext.Properties["idUser"] = aktBenutzer.Id;

                            aktBenutzer.Passwort = Tools.GenerierePasswort(neuesPassword);
                            context.SaveChanges();

                            result = BenutzerAdministrator.Passwortwechselergebnis.Success;
                            log.Info("Passwort aufgrund altem Passwort erfolgreich geändert!");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Fehler bei BenutzerPasswortÄndern", ex);
                        if (ex.InnerException != null)
                            log.Error("Fehler bei BenutzerPasswortÄndern (inner)", ex.InnerException);
                        throw;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Ermittelt den Benutzer via der Emailadresse
        /// </summary>
        /// <param name="emailadress"></param>
        public static Benutzer SucheFirmaVonBenutzer(string emailadress)
        {
            log.Info("BenutzerVerwaltung - LoadCompanyOfAUser");
            Benutzer user = new Benutzer();
            if (!string.IsNullOrEmpty(emailadress))
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {                       
                        user = context.AlleBenutzer.Where(x => x.Emailadresse == emailadress).FirstOrDefault();
                        if (user != null)
                        {
                            log.Error("BenutzerVerwaltung - LadeFirmaVonBenutzer - the company with the fa_id was not found");
                        }
                        else
                        {
                            log.Error("BenutzerVerwaltung - LadeFirmaVonBenutzer - the user with the emailadress was not found");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Connection to database failed");
                    log.Info(ex.Message, ex.InnerException);
                }
            }
            return user;
        }

        /// <summary>
        /// Sucht alle Mitarbeiter einer Firma heraus und sendet diese zurück
        /// </summary>
        /// <param name="emailadresse"></param>
        /// <returns></returns>
        public static List<Benutzer> LadeMitarbeiterEinerFirma(int fa_id)
        {
            List<Benutzer> AllStuffofCompany = new List<Benutzer>();
            log.Info("BenutzerVerwaltung - LoadStuffOfACompany");
            if (fa_id > 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {

                        AllStuffofCompany = context.AlleBenutzer.Where(x => x.Firma_id == fa_id).ToList();
                        if (AllStuffofCompany.Count < 1)
                        {
                            log.Error("BenutzerVerwaltung - LoadStuffOfACompany - There is a mistake by finding stuff from a company");
                        }
                        else
                        {
                            log.Info("BenutzerVerwaltung - LoadStuffOfACompany - Loading stuff was successful");                            
                        }

                    }
                }
                catch (Exception ex)
                {
                    log.Error("BenutzerVerwaltung - LoadStuffOfACompany - The connection to database was not possible");
                    log.Info(ex.Message, ex.InnerException);
                }
            }
            return AllStuffofCompany;
        }

        /// <summary>
        /// ladet alle Firmen die vorhanden sind
        /// </summary>
        /// <returns></returns>
        public static List<Firma> LadeAlleFirmen()
        {
            List<Firma> alleFirmen = null;
            log.Info("BenutzerVerwaltung - LadeAlleFirmen");
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    alleFirmen = context.AlleFirmen.ToList();
                    if (alleFirmen == null)
                    {
                        log.Error("BenutzerVerwaltung - ");
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("BenutzerVerwaltung - LadeAllefirmen - konnte nicht geladen werden");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return alleFirmen;
        }

        /// <summary>
        /// Findet den Benutzer zu einer  Emailadresse und setzt diesen auf inaktiv
        /// </summary>
        /// <param name="emailadresse"></param>
        /// <returns></returns>
        public static bool DeaktiviereBenutzer(string emailadresse)
        {
            bool erfolgreich = false;
            Benutzer aktBenutzer = null;
            log.Info("DeaktiviereBenuter(emailadresse)");
            if (string.IsNullOrEmpty(emailadresse))
            {
                return erfolgreich;
            }
            else
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        aktBenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == emailadresse).FirstOrDefault();
                        aktBenutzer.Aktiv = false;
                        int anzahlAenderung = context.SaveChanges();
                        if (anzahlAenderung > 0)
                        {
                            erfolgreich = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                    log.Error("BenutzerVerwaltung-DeaktiviereBenutzer missed");
                    log.Info(ex.Message, ex.InnerException);
                }
            }
            return true;
        }

        /// <summary>
        /// Findet den Benutzer zu einer  Emailadresse und setzt diesen auf aktiv
        /// </summary>
        /// <param name="emailadresse"></param>
        /// <returns></returns>
        public static bool AktiviereBenutzer(string emailadresse)
        {
            Benutzer aktBenutzer = null;
            bool erfolgreich = false;
            log.Info("BenutzerVerwaltung - AktiviereBenutzer");
            if (!string.IsNullOrEmpty(emailadresse))
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {
                        aktBenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == emailadresse).FirstOrDefault();
                        aktBenutzer.Aktiv = true;
                        int anzahlAenderung = context.SaveChanges();
                        if (anzahlAenderung > 0)
                        {
                            erfolgreich = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("BenutzerVerwaltung - AktiviereBenutzer fahlgeschlagen");
                    log.Info(ex.Message, ex.InnerException);

                }
            }
            return erfolgreich;
        }

        /// <summary>
        /// aktualisiert Vorname und Nachname eines Benutzers
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vorname"></param>
        /// <param name="nachname"></param>
        /// <returns></returns>
        public static bool AktualisiereBenutzer(int id, string vorname, string nachname)
        {
            log.Info("BenutzerVerwaltung - AktualisiereBenutzer");
            Benutzer aktBenutzer = new Benutzer();
            bool erfolgreich = false;

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    ///
                    aktBenutzer = context.AlleBenutzer.Where(x => x.Id == id).FirstOrDefault();
                    aktBenutzer.Nachname = nachname;
                    aktBenutzer.Vorname = vorname;

                    int save = context.SaveChanges();
                    if (save>0)
                    {
                        erfolgreich = true;
                    }
                    else
                    {
                        log.Warn("BenutzerVerwaltung - AktualisiereBenutzer - speichern hat nicht geklappt");
                    }

                }
            }
            catch (Exception ex )
            {
                log.Error("Benutzerverwaltung - AktualisiereBenutzer - Datenbankzugriff hat nicht geklappt");
                if (ex.InnerException!=null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return erfolgreich;
        }

        /// <summary>
        /// aktutlisiert alle Daten eines Mitarbeiters und bestätigt dies, wenn es geklappt hat
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emailadresse"></param>
        /// <param name="vorname"></param>
        /// <param name="nachname"></param>
        /// <param name="aktiv"></param>
        /// <returns></returns>
        public static bool AktualisiereMitarbeiterEinerFirma(int id, string emailadresse, string vorname, string nachname, bool aktiv)
        {
            log.Info("BenutzerVerwaltung - AktualisiereMitarbeiterEinerFirma");
            Benutzer aktBenutzer = new Benutzer();
            bool erfolgreich = false;

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    ///
                    aktBenutzer = context.AlleBenutzer.Where(x => x.Id == id).FirstOrDefault();
                    aktBenutzer.Nachname = nachname;
                    aktBenutzer.Vorname = vorname;
                    aktBenutzer.Aktiv = aktiv;
                    aktBenutzer.Emailadresse = emailadresse;

                    int save = context.SaveChanges();
                    if (save > 0)
                    {
                        erfolgreich = true;
                    }
                    else
                    {
                        log.Warn("BenutzerVerwaltung - AktualisiereBenutzer - speichern hat nicht geklappt");
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("Benutzerverwaltung - AktualisiereBenutzer - Datenbankzugriff hat nicht geklappt");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return erfolgreich;
        }

        public static bool LegeMitarbeiterAn(int fa_id, string emailadresse, string vorname, string nachname, string password, string rolle)
        {
            log.Info("BenutzerVerwaltung - LegeMitarbeiterAn");
            Benutzer aktBenutzer = new Benutzer();
            bool erfolgreich = false;
           
            try
            {
                using (var context = new Innovation4AustriaEntities())
                {                   

                    aktBenutzer.Firma_id = fa_id;
                    aktBenutzer.Firma = context.AlleFirmen.Where(x => x.Id == fa_id).FirstOrDefault();
                    aktBenutzer.Nachname = nachname;
                    aktBenutzer.Vorname = vorname;
                    aktBenutzer.Passwort = Tools.GenerierePasswort(password);
                    aktBenutzer.Emailadresse = emailadresse;
                    aktBenutzer.Rolle = context.AlleRollen.Where(x => x.Bezeichnung == rolle).FirstOrDefault();
                    aktBenutzer.Aktiv = true;
                    context.AlleBenutzer.Add(aktBenutzer);
                   

                    int save = context.SaveChanges();
                    if (save > 0)
                    {
                        erfolgreich = true;
                    }
                    else
                    {
                        log.Warn("BenutzerVerwaltung - LegeMitarbeiterAn - speichern hat nicht geklappt");
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("Benutzerverwaltung - LegeMitarbeiterAn - Datenbankzugriff hat nicht geklappt");
                if (ex.InnerException != null)
                {
                    log.Info(ex.InnerException);
                }
            }
            return erfolgreich;
        }
    }
}

