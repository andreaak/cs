using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services;

namespace TestSecurity.BusinessLogic
{
    public static class Constants
    {
        public const string uniqueAppName = "TestSecurity";
        public const string authentificationServiceUrl = "AuthentificationServiceUrl";
        public const string users = "Users";
        public const string roles = "Roles";
        public const string adminRole = "Admin";
        public const string managerRole = "Manager";
        public const string superUserRole = "SuperUser";
        public const string adminLogin = "admin";
        public const string adminPassword = "admin!";

        public static string[] GetPredefinedRoles()
        {
            return new string[] { adminRole, managerRole, superUserRole };
        }

        public static SimpleUser GetPredefinedAdmin()
        {
            return new SimpleUser 
            {
                Login = adminLogin,
                Password = adminPassword
            };
        }
    }
}