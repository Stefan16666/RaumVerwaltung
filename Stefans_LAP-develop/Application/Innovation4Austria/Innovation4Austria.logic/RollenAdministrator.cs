using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Innovation4Austria.logic;

namespace Innovation4Austria.logic
{
    public class RollenAdministrator
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// liefert alle user die der Rolle entsprechen
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static List<Benutzer> GetRoleUsers(string roleName)
        {
            log.Info("GetRoleUsers(rolenName)");

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            else
            {
                List<Benutzer> roleUsers = null;

                using (var context = new Innovation4AustriaEntities())
                {
                    try
                    {
                        Rolle aktRolle = context.AlleRollen.Where(x => x.Bezeichnung == roleName).FirstOrDefault();
                        if (aktRolle != null)
                        {
                            roleUsers = aktRolle.AlleBenutzer.Where(x => x.Aktiv==true).ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in GetRoleUsers", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in GetRoleUsers (inner)", ex.InnerException);
                        throw;
                    }
                }

                return roleUsers;
            }
        }

        /// <summary>
        /// líefert alle Rollen
        /// </summary>
        /// <returns></returns>
        public static List<Rolle> GetRoles()
        {
            log.Info("GetRoles()");
            List<Rolle> rollen = null;

            using (var context = new Innovation4AustriaEntities())
            {
                try
                {
                    rollen = context.AlleRollen.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetRoles", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetRoles (inner)", ex.InnerException);
                    throw;
                }
            }

            return rollen;
        }

        /// <summary>
        /// liefert anhand der Emailadresse die Rolle des Benutzers
        /// </summary>
        /// <param name="emailadresse"></param>
        /// <returns></returns>
        public static Rolle GetUserRole(string emailadresse)
        {
            log.Info("GetUserRoles(username)");

            if (string.IsNullOrEmpty(emailadresse))
            {
                throw new ArgumentNullException(nameof(emailadresse));
            }
            else
            {
                Rolle userRole = null;

                using (var context = new Innovation4AustriaEntities())
                {
                    try
                    {
                        Benutzer aktBenutzer = context.AlleBenutzer.Where(x => x.Emailadresse == emailadresse).FirstOrDefault();
                        if (aktBenutzer != null)
                        {
                            userRole = aktBenutzer.Rolle;
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in GetUserRole", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in GetUserRole (inner)", ex.InnerException);
                        throw;
                    }
                }

                return userRole;
            }
        }
    }
}
