using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Innovation4Austria.logic
{
    public class Tools
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Generiert mit Hilfe des SHA512 ein Password (ByteArray)
        /// </summary>
        /// <param name="passwort"></param>
        /// <returns></returns>
        public static byte[] GenerierePasswort(string passwort)
        {
            log.Info("Innovation4Austria.logic - GenerierePasswort");

            SHA512 hash = SHA512.Create();

            byte[] pw = hash.ComputeHash(Encoding.UTF8.GetBytes(passwort));

            return pw;


        }
        /// <summary>
        /// Prüft ob eine Email schon vergeben worden ist, wenn ja, dann gibt true zurück
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool EmailVorhanden(string email)
        {
            log.Info("Tools - Email Vorhanden");        

            bool emailVorhanden = false;          

            try
            {
                using (var context = new Innovation4AustriaEntities())
                {
                    emailVorhanden = context.AlleBenutzer.Any(x => x.Emailadresse == email);
                }
            }
            catch (Exception ex)
            {
                log.Error("Tools - EmailVorhanden - DB-Verbindung fehlgeschlagen");
                if (ex.InnerException!=null)
                {
                    log.Info(ex.InnerException);
                }
            }           
            return emailVorhanden;
        }
    }
}

