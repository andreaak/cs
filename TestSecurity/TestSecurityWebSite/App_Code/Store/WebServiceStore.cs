using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Services;

namespace TestSecurity.BusinessLogic.Store
{
    public class WebServiceStore
    {
        AuthentificationService service;
        public WebServiceStore()
        {
            service = new AuthentificationService();
            service.Url = ConfigurationManager.AppSettings[Constants.authentificationServiceUrl];
        }

        #region PUBLIC

        public SimpleUser AddUser(string login, string password)
        {
            string errorMessage = null;
            try
            {
                UserResponse response = service.AddUser(Constants.uniqueAppName, login, password);
                if (string.IsNullOrEmpty(response.ErrorMessage))
                {
                    return response.User;
                }
                errorMessage = response.ErrorMessage;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            throw new Exception(errorMessage);
        }

        public SimpleUser GetUserByName(string login)
        {
            string errorMessage = null;
            try
            {
                UserResponse response = service.GetUserByName(Constants.uniqueAppName, login);
                if(string.IsNullOrEmpty(response.ErrorMessage))
                {
                    return response.User;
                }
                errorMessage = response.ErrorMessage;
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
            throw new Exception(errorMessage);
        }

        public SimpleRole AddRole(string roleName)
        {
            string errorMessage = null;
            try
            {
                RoleResponse response = service.AddRole(Constants.uniqueAppName, roleName);
                if (string.IsNullOrEmpty(response.ErrorMessage))
                {
                    return response.Role;
                }
                errorMessage = response.ErrorMessage;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            throw new Exception(errorMessage);
        }

        public SimpleRole[] AddRoles(string[] roles)
        {
            string errorMessage = null;
            try
            {
                RolesResponse response = service.AddRoles(Constants.uniqueAppName, roles);
                if (string.IsNullOrEmpty(response.ErrorMessage))
                {
                    return response.Roles;
                }
                errorMessage = response.ErrorMessage;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            throw new Exception(errorMessage);
        }

        public SimpleRole[] GetRoles()
        {
            string errorMessage = null;
            try
            {
                RolesResponse response = service.GetRoles(Constants.uniqueAppName);
                if (string.IsNullOrEmpty(response.ErrorMessage))
                {
                    return response.Roles;
                }
                errorMessage = response.ErrorMessage;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            throw new Exception(errorMessage);
        }

        public SimpleRole[] AddUsersToRoles(SimpleRole[] roles)
        {
            string errorMessage = null;
            try
            {
                RolesResponse response = service.AddUsersToRoles(Constants.uniqueAppName, roles);
                if (string.IsNullOrEmpty(response.ErrorMessage))
                {
                    return roles;
                }
                errorMessage = response.ErrorMessage;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            throw new Exception(errorMessage);
        }

        #endregion
    }
}