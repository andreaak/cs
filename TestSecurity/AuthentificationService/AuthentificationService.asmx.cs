using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AuthentificationService.BusinessLogic;
using AuthentificationService.BusinessLogic.Store;
using AuthentificationService.BusinessLogic.ServiceObjects;

namespace AuthentificationService
{
    /// <summary>
    /// Summary description for AuthentificationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AuthentificationService : System.Web.Services.WebService
    {

        [WebMethod]
        public string TestDatabase()
        {
            using (DBProvider provider = new DBProvider())
            {
                string error = provider.TestConnection();
                if (string.IsNullOrEmpty(error))
                {
                    return "Database Ok";
                }
                return error;
            }
        }

        [WebMethod]
        public UserResponse AddUser(string applicationId, string login, string password)
        {
            CacheStore store = new CacheStore();
            return store.AddUser(applicationId, login, password);
        }

        [WebMethod]
        public UserResponse GetUserByName(string applicationId, string login)
        {
            CacheStore store = new CacheStore();
            return store.GetUserByName(applicationId, login);
        }

        [WebMethod]
        public RoleResponse AddRole(string applicationId, string roleName)
        {
            CacheStore store = new CacheStore();
            return store.AddRole(applicationId, roleName);
        }

        [WebMethod]
        public RolesResponse AddRoles(string applicationId, string[] roleNames)
        {
            CacheStore store = new CacheStore();
            return store.AddRoles(applicationId, roleNames);
        }

        [WebMethod]
        public RolesResponse GetRoles(string applicationId)
        {
            CacheStore store = new CacheStore();
            return store.GetRoles(applicationId);
        }

        [WebMethod]
        public RolesResponse AddUsersToRoles(string applicationId, SimpleRole[] roles)
        {
            CacheStore store = new CacheStore();
            return store.AddUsersToRoles(applicationId, roles);
        }

    }
}
