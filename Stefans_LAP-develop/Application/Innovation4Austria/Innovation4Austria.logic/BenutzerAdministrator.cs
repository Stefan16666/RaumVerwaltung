using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Data.Entity;

namespace Innovation4Austria.logic
{
    public class BenutzerAdministrator
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public enum LogonResult
        {
            LogonDataValid,
            LogonDataInvalid,
            UserInactive,
            UnkownUser,
        }

        public enum Passwortwechselergebnis
        {
            Success,
            UserInactive,
            UsernameInvalid,
            PasswortInvalid
        }

        public enum ProfileChangeResult
        {
            Success,
            UserInactive,
            UsernameInvalid
        }


        /// <summary>
        /// wechselt das Passwort
        /// </summary>
        /// <param name="username"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static Passwortwechselergebnis WechselPasswort(string username, string oldPassword, string newPassword)
        {
            Passwortwechselergebnis result = Passwortwechselergebnis.UsernameInvalid;

            log.Info("ChangePassword(username, oldPassword, newPassword)");

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            else if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(nameof(newPassword));
            else if (string.IsNullOrEmpty(oldPassword))
                throw new ArgumentNullException(nameof(oldPassword));
            else
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    try
                    {
                        Benutzer aktBenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == username).FirstOrDefault();

                        if (aktBenutzer == null)
                        {
                            result = Passwortwechselergebnis.UsernameInvalid;
                        }
                        else if (!aktBenutzer.Aktiv==true)
                        {
                            result = Passwortwechselergebnis.UserInactive;
                        }
                        else if (!aktBenutzer.Passwort.SequenceEqual(Tools.GenerierePasswort(oldPassword)))
                        {
                            result = Passwortwechselergebnis.PasswortInvalid;
                        }
                        else
                        {
                            log4net.LogicalThreadContext.Properties["idUser"] = aktBenutzer.Id;

                            aktBenutzer.Passwort = Tools.GenerierePasswort(newPassword);
                            context.SaveChanges();

                            result = Passwortwechselergebnis.Success;
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
        /// Setzt den Benutzer auf inaktiv
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool DeaktiviereBenutzer(string username)
        {
            log.Info("DeactivateUser(username)");
            bool success = false;

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
            else
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    try
                    {
                        Benutzer aktBenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == username).FirstOrDefault();

                        if (aktBenutzer != null)
                        {
                            aktBenutzer.Aktiv = false;
                            context.SaveChanges();
                            success = true;
                            log.Info("User has been deactivated!");
                        }
                        else
                        {
                            log.Info("Unknown username");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in DeactivateUser", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in DeactivateUser (inner)", ex.InnerException);
                        throw;
                    }
                }
            }
            return success;
        }

        /// <summary>
        ///  setzt den Benutzer auf aktiv
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool AktiviereBenutzer(string username)
        {
            log.Info("ActivateUser(username)");
            bool success = false;

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
            else
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    try
                    {
                        Benutzer aktBanutzer = context.AlleBenutzer.Where(x => x.Emailadresse == username).FirstOrDefault();

                        if (aktBanutzer != null)
                        {
                            aktBanutzer.Aktiv = true;
                            context.SaveChanges();
                            success = true;
                            log.Info("User has been deactivated!");
                        }
                        else
                        {
                            log.Info("Unknown username");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in DeactivateUser", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in DeactivateUser (inner)", ex.InnerException);
                       
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// findet den Benutzer anhand von Emailadresse und Password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static LogonResult Anmelden(string username, string password)
        {
            log.Info("Logon(username, password)");
            LogonResult result = LogonResult.LogonDataInvalid;

            if (string.IsNullOrEmpty(username))
            {
                log.Error("Username is empty!");
                throw new ArgumentNullException(nameof(username));
            }
            else if (string.IsNullOrEmpty(password))
            {
                log.Error("Password is empty!");
                throw new ArgumentNullException(nameof(password));
            }
            else
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    try
                    {

                        Benutzer aktBenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == username).FirstOrDefault();

                        if (aktBenutzer != null)
                        {
                            if (aktBenutzer.Passwort.SequenceEqual(Tools.GenerierePasswort(password)))
                            {
                                if (!aktBenutzer.Aktiv== true)
                                {
                                    log.Info("User inactive");
                                    result = LogonResult.UserInactive;
                                }
                                else
                                {
                                    log.Info("Logon data valid");
                                    result = LogonResult.LogonDataValid;
                                }
                            }
                            else
                            {
                                log.Info("Logon data invalid");
                                result = LogonResult.LogonDataInvalid;
                            }

                            int anzahlZeilen = context.SaveChanges();
                        }
                        else
                        {
                            result = LogonResult.UnkownUser;
                            log.Info("Unknown username");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in Logon", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in Logon (inner)", ex.InnerException);
                        
                    }
                }
            }
            return result;
        }
        
        /// <summary>
        /// Findet den User anhand der Emailadresse
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static Benutzer GetUser(string username)
        {
            log.Info("GetUser(username)");

            Benutzer user = null;

            using (var context = new Innovation4AustriaEntities())
            {
                try
                {
                  
                    user = context.AlleBenutzer.Include(x => x.Firma).Include(x => x.Rolle).Where(x => x.Emailadresse == username).FirstOrDefault();

                    if (user == null)
                    {
                        log.Info("Unknown username!");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetUser", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetUser (inner)", ex.InnerException);                 
                }
            }

            return user;
        }
    }

}

