using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Security;
using TestSecurity.BusinessLogic.Store;
using Services;

namespace TestSecurity.BusinessLogic.SecurityProviders
{
    public class WebServiceMembershipProvider : MembershipProvider
    {
        private CacheStore currentStore = null;
        private string applicationName;
        private bool enablePasswordReset;
        private bool requiresQuestionAndAnswer;
        private string passwordStrengthRegEx;
        private int maxInvalidPasswordAttempts;
        private int minRequiredNonAlphanumericChars;
        private int minRequiredPasswordLength;
        private MembershipPasswordFormat masswordFormat;

        #region PROPERTIES

        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
                currentStore = null;
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                return enablePasswordReset;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                if (this.PasswordFormat == MembershipPasswordFormat.Hashed)
                    return false;
                else
                    return true;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return maxInvalidPasswordAttempts;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return minRequiredNonAlphanumericChars;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return minRequiredPasswordLength;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return 20;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return masswordFormat;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return passwordStrengthRegEx;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return requiresQuestionAndAnswer;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return true;
            }
        }

        private CacheStore CurrentStore
        {
            get
            {
                if (currentStore == null)
                {
                    currentStore = CacheStore.Instance;
                }
                return currentStore;
            }
        }

        #endregion

        #region OVERRIDE

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "WebServiceMembershipProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "WebService Membership Provider");
            }

            // Initialize the base class
            base.Initialize(name, config);

            // Initialize default values
            applicationName = Constants.uniqueAppName;
            enablePasswordReset = false;
            passwordStrengthRegEx = @"[\w| !§$%&/()=\-?\*]*";
            maxInvalidPasswordAttempts = 3;
            minRequiredNonAlphanumericChars = 1;
            minRequiredPasswordLength = 5;
            requiresQuestionAndAnswer = false;
            masswordFormat = MembershipPasswordFormat.Encrypted;

            // Now go through the properties and initialize custom values
            foreach (string key in config.Keys)
            {
                switch (key.ToLower())
                {
                    case "name":
                        name = config[key];
                        break;
                    case "applicationname":
                        applicationName = config[key];
                        break;
                    case "enablepasswordreset":
                        enablePasswordReset = bool.Parse(config[key]);
                        break;
                    case "passwordstrengthregex":
                        passwordStrengthRegEx = config[key];
                        break;
                    case "maxinvalidpasswordattempts":
                        maxInvalidPasswordAttempts = int.Parse(config[key]);
                        break;
                    case "minrequirednonalphanumericchars":
                        minRequiredNonAlphanumericChars = int.Parse(config[key]);
                        break;
                    case "minrequiredpasswordlength":
                        minRequiredPasswordLength = int.Parse(config[key]);
                        break;
                    case "passwordformat":
                        masswordFormat = (MembershipPasswordFormat)Enum.Parse(
                                    typeof(MembershipPasswordFormat), config[key]);
                        break;
                    case "requiresquestionandanswer":
                        requiresQuestionAndAnswer = bool.Parse(config[key]);
                        break;
                }
            }
        }

        public override MembershipUser CreateUser(string username, string password,
            string email, string passwordQuestion,
            string passwordAnswer, bool isApproved,
            object providerUserKey, out MembershipCreateStatus status)
        {

            if (!ValidateUsername(username))
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }

            // Raise the event before validating the password
            base.OnValidatingPassword(
                new ValidatePasswordEventArgs(
                        username, password, true));

            // Validate the password
            if (!ValidatePassword(password))
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            // Add the user to the store
            SimpleUser user = CurrentStore.AddUser(username, TransformPassword(password));
            if (user != null)
            {
                status = MembershipCreateStatus.Success;
                return CreateMembershipFromInternalUser(user);
            }
            status = MembershipCreateStatus.ProviderError;
            return null;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            SimpleUser user = CurrentStore.GetUserByName(username);
            if (user != null)
            {
                return CreateMembershipFromInternalUser(user);
            }
            else
            {
                return null;
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            SimpleUser user = CurrentStore.GetUserByName(username);
            return user != null && ValidateUserInternal(user, password);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotSupportedException();
        }
        
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotSupportedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotSupportedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region PRIVATE

        private string TransformPassword(string password)
        {
            //PssswordTransformation
            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    return password;
                case MembershipPasswordFormat.Hashed:
                    return password;
                case MembershipPasswordFormat.Encrypted:
                    return password;
                default:
                    return password;
            }
        }

        private bool ValidateUsername(string userName)
        {
            return CurrentStore.GetUserByName(userName) == null;
        }

        private bool ValidatePassword(string password)
        {
            bool IsValid = true;
            Regex HelpExpression;

            // Validate simple properties
            IsValid = IsValid && (password.Length >= this.MinRequiredPasswordLength);

            // Validate non-alphanumeric characters
            HelpExpression = new Regex(@"\W");
            IsValid = IsValid && (HelpExpression.Matches(password).Count >= this.MinRequiredNonAlphanumericCharacters);

            // Validate regular expression
            HelpExpression = new Regex(this.PasswordStrengthRegularExpression);
            IsValid = IsValid && (HelpExpression.Matches(password).Count > 0);

            return IsValid;
        }

        private bool ValidateUserInternal(SimpleUser user, string password)
        {
            if (user != null)
            {
                return password == TransformPassword(user.Password);
            }
            return false;
        }

        private MembershipUser CreateMembershipFromInternalUser(SimpleUser user)
        {
            MembershipUser muser = new MembershipUser(base.Name,
                user.Login, CreateKey(user.Login), string.Empty, string.Empty,
                string.Empty, true, false, DateTime.Now, DateTime.Now,
                DateTime.Now, DateTime.Now, DateTime.MaxValue);

            return muser;
        }

        private MembershipUserCollection CreateMembershipCollectionFromInternalList(List<SimpleUser> users)
        {
            MembershipUserCollection ReturnCollection = new MembershipUserCollection();

            foreach (SimpleUser user in users)
            {
                ReturnCollection.Add(CreateMembershipFromInternalUser(user));
            }

            return ReturnCollection;
        }

        private string CreateKey(string username)
        {
            return Constants.uniqueAppName + username;
        }

        #endregion
    }
}