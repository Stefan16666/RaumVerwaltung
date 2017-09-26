using System;
using Innovation4Austria.logic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Innovation4austria.authentication
{
    public class i4aRoleProvider : RoleProvider
    {
        string applicationName = "innovation4austria database";
        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                    applicationName = value;
            }
        }

        public override string[] FindUsersInRole(string rollenName, string gesuchterBenutzer)
        {
            List<Benutzer> Rollenbenutzer = RollenAdministrator.GetRoleUsers(gesuchterBenutzer);
            if (Rollenbenutzer != null)
            {
                return Rollenbenutzer.Where(x => x.Emailadresse.Contains(gesuchterBenutzer)).Select(x => x.Emailadresse).ToArray();
            }
            else
            {
                return null;
            }
        }

        public override string[] GetAllRoles()
        {
            List<Rolle> allRoles = RollenAdministrator.GetRoles();

            if (allRoles != null)
            {
                return allRoles.Select(x => x.Bezeichnung).ToArray();
            }
            else
            {
                return null;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            Rolle userRoles = RollenAdministrator.GetUserRole(username);

            if (userRoles != null)
            {
                return new string[] { userRoles.Bezeichnung };
            }
            else
            {
                return null;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {

            List<Benutzer> roleUsers = RollenAdministrator.GetRoleUsers(roleName);
            if (roleUsers != null)
            {
                return roleUsers.Select(x => x.Emailadresse).ToArray();
            }
            else
            {
                return null;
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            string[] userRoles = GetRolesForUser(username);
            if (userRoles != null)
            {
                return userRoles.Contains(roleName);
            }
            else
            {
                return false;
            }
        }

        public override bool RoleExists(string roleName)
        {
            string[] allRoles = GetAllRoles();
            if (allRoles != null)
            {
                return allRoles.Contains(roleName);
            }
            else
            {
                return false;
            }
        }

        #region NotImplementedMember
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
