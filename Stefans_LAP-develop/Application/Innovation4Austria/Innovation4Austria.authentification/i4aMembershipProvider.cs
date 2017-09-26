
using System;
using Innovation4Austria.logic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Verwaltung;



namespace Innovation4Austria.authentication
{
    public class i4aMembershipProvider : MembershipProvider
    {
        string applicationName = "innovation4austria database";
        const int MAX_INVALID_PASSWORT_ATTEMPTS = 3;
        const int MIN_REQUIRED_NON_ALPHANUMERIC_CHARACTERS = 0;
        const int MIN_REQUIRED_PASSWORD_LENGTH = 8;
        const int PASSWORD_ATTEMPT_WINDOW = 10;
        const bool REQUIRES_QUESTION_AND_ANSWER = false;
        const bool REQUIRES_UNIQUE_EMAIL = true;

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

        public override bool EnablePasswordReset
        {
            get
            {
                return false;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return false;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return MAX_INVALID_PASSWORT_ATTEMPTS;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return MIN_REQUIRED_NON_ALPHANUMERIC_CHARACTERS;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return MIN_REQUIRED_PASSWORD_LENGTH;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return PASSWORD_ATTEMPT_WINDOW;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return MembershipPasswordFormat.Clear;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                /// TODO - Anpassung Regular Expression für Passwort
                return "[a-zA-Z0-9]{8,*}";
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return REQUIRES_QUESTION_AND_ANSWER;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return REQUIRES_QUESTION_AND_ANSWER;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return BenutzerAdministrator.WechselPasswort(username, oldPassword, newPassword) == Innovation4Austria.logic.BenutzerAdministrator.Passwortwechselergebnis.Success;
        }

        public override bool DeleteUser(string emailadresse, bool deleteAllRelatedData)
        {
            return BenutzerVerwaltung.DeaktiviereBenutzer(emailadresse);
        }

        public override bool UnlockUser(string userName)
        {
            return BenutzerVerwaltung.AktiviereBenutzer(userName);
        }

        public override bool ValidateUser(string username, string password)
        {
            return BenutzerAdministrator.Anmelden(username, password) == BenutzerAdministrator.LogonResult.LogonDataValid;
        }

        #region NotImplementedMember
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
