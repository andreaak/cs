using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Security;
using TestSecurity.BusinessLogic.Store;

namespace TestSecurity.BusinessLogic.SecurityProviders
{
    public class WebServiceRoleProvider : RoleProvider
    {
        private string applicationName;

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

        private CacheStore currentStore = null;
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

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "WebServiceRoleProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "WebService Role Provider");
            }

            // Base initialization
            base.Initialize(name, config);

            // Initialize properties
            applicationName = Constants.uniqueAppName;
            foreach (string key in config.Keys)
            {
                if (key.ToLower().Equals("applicationname"))
                    ApplicationName = config[key];
            }

            if (CurrentStore.GetAllRoles().Length == 0)
            {
                CurrentStore.SaveInitialData();
            }
        }

        public override void CreateRole(string roleName)
        {
            CurrentStore.AddRole(roleName);
        }
        
        public override bool RoleExists(string roleName)
        {
            return CurrentStore.GetRole(roleName) != null;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            string[] allRoles = CurrentStore.GetAllRoles();
            if (roleNames.Any(item => !allRoles.Contains(item)))
            {
                throw new ProviderException("Some roles does not exist!");
            }
            CurrentStore.AddUsersToRoles(usernames, roleNames);
        }

        public override string[] GetAllRoles()
        {
            return CurrentStore.GetAllRoles();
        }

        public override string[] GetRolesForUser(string username)
        {
            return CurrentStore.GetRolesForUser(username);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return CurrentStore.GetUsersInRole(roleName);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return CurrentStore.GetUsersInRole(roleName).Contains(username);
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            List<string> results = new List<string>();
            Regex Expression = new Regex(usernameToMatch.Replace("%", @"\w*"));
            string[] users = CurrentStore.GetUsersInRole(roleName);
            foreach (string userName in users)
            {
                if (Expression.IsMatch(userName))
                {
                    results.Add(userName);
                }
            }
            return results.ToArray();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotSupportedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}