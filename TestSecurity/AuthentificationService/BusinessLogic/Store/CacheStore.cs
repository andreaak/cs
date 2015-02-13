using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthentificationService.BusinessLogic.ServiceObjects;

namespace AuthentificationService.BusinessLogic.Store
{
    public class CacheStore
    {
        private readonly DBStore dbStore = null;

        public CacheStore()
        {
            dbStore = new DBStore();
        }
        
        #region USER

        public UserResponse GetUserByName(string applicationId, string login)
        {
            UserResponse res = new UserResponse();
            SimpleUser user = GetAllUsersInCache(applicationId).FirstOrDefault(item => item.Login == login);
            if(user != null)
            {
                res.User = user;
                res.IsCacheable = true;
            }
            else
            {
                try
                {
                    user = dbStore.GetUserByName(applicationId, login);
                    res.User = user;
                }
                catch (Exception ex)
                {
                    res.ErrorMessage = ex.Message;
                }
            }
            return res;
        }

        public UserResponse AddUser(string applicationId, string login, string password)
        {
            UserResponse res = new UserResponse();
            try
            {
                if (dbStore.AddUser(applicationId, login, password))
                {
                    res.User = new SimpleUser
                    {
                        Login = login,
                        Password = password
                    };
                    AddUserToCache(applicationId, res.User);
                    res.IsCacheable = false;
                }
                else
                {
                    res.ErrorMessage = "User is not added";
                }
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
            }
            return res;
        }

        #endregion
        
        #region ROLE

        public RoleResponse AddRole(string applicationId, string roleName)
        {
            RoleResponse res = new RoleResponse();
            try
            {
                if (dbStore.AddRole(applicationId, roleName))
                {
                    res.Role = new SimpleRole
                    {
                        RoleName = roleName,
                        AssignedUsers = new HashSet<string>()
                    };
                    AddRolesToCache(applicationId, new SimpleRole[] { res.Role });
                    res.IsCacheable = false;
                }
                else
                {
                    res.ErrorMessage = "Role is not added";
                }
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
            }
            return res;
        }

        public RolesResponse AddRoles(string applicationId, string[] roleNames)
        {
            RolesResponse res = new RolesResponse();
            try
            {
                dbStore.AddRoles(applicationId, roleNames);
                HashSet<SimpleRole> roles = new HashSet<SimpleRole>();
                foreach (string roleName in roleNames)
                {
                    roles.Add(new SimpleRole
                    {
                        RoleName = roleName,
                        AssignedUsers = new HashSet<string>()
                    });
                }
                res.Roles = roles.ToArray();
                AddRolesToCache(applicationId, res.Roles);
                res.IsCacheable = false;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
            }
            return res;
        }

        public RolesResponse GetRoles(string applicationId)
        {
            RolesResponse res = new RolesResponse();
            try
            {
                res.Roles = GetAllRoles(applicationId).ToArray();
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
            }
            return res;
        }

        public RolesResponse AddUsersToRoles(string applicationId, SimpleRole[] roles)
        {
            RolesResponse res = new RolesResponse();
            try
            {
                dbStore.AddUsersToRoles(applicationId, roles);
                UpdateRolesInCache(applicationId, roles);
                res.IsCacheable = false;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
            }
            return res;
        }

        #endregion

        #region PRIVATE

        private void AddUserToCache(string applicationId, SimpleUser user)
        {
            CacheContainer container;
            object data = HttpContext.Current.Cache[applicationId];
            if (data != null)
            {
                container = (CacheContainer)data;
            }
            else
            {
                container = new CacheContainer();
            }
            container.Users.Add(user);
            HttpContext.Current.Cache[applicationId] = container;
        }

        private void AddRolesToCache(string applicationId, IEnumerable<SimpleRole> newRoles)
        {
            CacheContainer container;
            object data = HttpContext.Current.Cache[applicationId];
            if (data != null)
            {
                container = (CacheContainer)data;
            }
            else
            {
                container = new CacheContainer();
            }
            container.Roles.UnionWith(newRoles);
            HttpContext.Current.Cache[applicationId] = container;
        }

        private void UpdateRolesInCache(string applicationId, IEnumerable<SimpleRole> updatedRoles)
        {
            CacheContainer container;
            object data = HttpContext.Current.Cache[applicationId];
            if (data != null)
            {
                container = (CacheContainer)data;
                foreach (SimpleRole updatedRole in updatedRoles)
                {
                    SimpleRole role = container.Roles.FirstOrDefault(item => item.RoleName == updatedRole.RoleName);
                    if (role == null)
                    {
                        container.Roles.Add(updatedRole);
                    }
                    else
                    {
                        role.AssignedUsers.UnionWith(updatedRole.AssignedUsers);
                    }
                }
            }
            else
            {
                container = new CacheContainer();
                container.Roles.UnionWith(updatedRoles);
            }
            HttpContext.Current.Cache[applicationId] = container;
        }

        private HashSet<SimpleRole> GetAllRoles(string applicationId)
        {
            object data = HttpContext.Current.Cache[applicationId];
            if (data == null)
            {
                HashSet<SimpleRole> roles = dbStore.GetRoles(applicationId);
                if (roles.Count != 0)
                {
                    AddRolesToCache(applicationId, roles);
                }
                return roles;
            }
            else
            {
                CacheContainer container = (CacheContainer)data;
                return container.Roles;
            }
        }

        private HashSet<SimpleUser> GetAllUsersInCache(string applicationId)
        {
            object data = HttpContext.Current.Cache[applicationId];
            if (data == null)
            {
                return new HashSet<SimpleUser>();
            }
            else
            {
                CacheContainer container = (CacheContainer)data;
                return container.Users;
            }
        }

        #endregion
    }
}