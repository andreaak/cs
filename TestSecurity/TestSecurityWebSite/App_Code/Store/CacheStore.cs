using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web;
using Services;

namespace TestSecurity.BusinessLogic.Store
{
    public class CacheStore
    {
        private readonly WebServiceStore webServiceStore = null;
        
        private static CacheStore  instance;
        public static CacheStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CacheStore();
                }
                return instance;
            }
        }

        private CacheStore ()
	    {
            webServiceStore = new WebServiceStore();
	    }

        #region USER

        public SimpleUser GetUserByName(string login)
        {
            SimpleUser user;
            object data = HttpContext.Current.Cache[Constants.users];
            if (data != null)
            {
                user = ((HashSet<SimpleUser>)data).FirstOrDefault(item => item.Login == login);
                if (user != null)
                {
                    return user;
                }
            }

            user = webServiceStore.GetUserByName(login);
            if (user != null)
            {
                AddUserToCache(user);
            }
            return user;
        }

        public SimpleUser GetUserByKey(string key)
        {
            throw new NotSupportedException();
        }

        public SimpleUser AddUser(string login, string password)
        {
            SimpleUser user = webServiceStore.AddUser(login, password);
            if (user != null)
            {
                AddUserToCache(user);
            }
            return user;
        }

        #endregion

        #region ROLE

        public void AddRole(string roleName)
        {
            SimpleRole role = GetRoles().FirstOrDefault(item => item.RoleName == roleName);
            if (role == null)
            {
                role = webServiceStore.AddRole(roleName);
                AddRolesToCache(new SimpleRole[] { role });
            }
        }

        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            SimpleRole[] roles = GetUpdatedRoles(usernames, roleNames);

            SimpleRole[] updatedRoles = webServiceStore.AddUsersToRoles(roles);
            UpdateRolesInCache(updatedRoles);
        }

        private SimpleRole[] GetUpdatedRoles(string[] usernames, string[] roleNames)
        {
            HashSet<SimpleRole> allRoles = GetRoles();
            List<SimpleRole> roles = new List<SimpleRole>();
            foreach (string roleName in roleNames)
            {
                SimpleRole role = allRoles.FirstOrDefault(item => item.RoleName == roleName);
                if (role == null)
                {
                    throw new ProviderException("Roles is not exist");
                }
                HashSet<string> newUsers = new HashSet<string>();
                foreach (string username in usernames)
                {
                    if (!role.AssignedUsers.Contains(username))
                    {
                        newUsers.Add(username);
                    }
                }

                if (newUsers.Count != 0)
                {
                    SimpleRole updatedRole = new SimpleRole
                    {
                        RoleName = roleName,
                        AssignedUsers = newUsers.ToArray()
                    };
                    roles.Add(updatedRole);
                }
            }
            return roles.ToArray();
        }

        public SimpleRole GetRole(string roleName)
        {
            SimpleRole role = GetRoles().FirstOrDefault(item => item.RoleName == roleName);
            if (role != null)
            {
                return role;
            }
            throw new ProviderException("Role does not exist!");
        }

        public string[] GetAllRoles()
        {
            return GetRoles().Select(item => item.RoleName).ToArray();
        }

        public string[] GetRolesForUser(string login)
        {
            return GetRoles()
                .Where(item => item.AssignedUsers.Contains(login))
                .Select(item => item.RoleName)
                .ToArray();
        }

        public string[] GetUsersInRole(string roleName)
        {
            SimpleRole role = GetRoles().FirstOrDefault(item => item.RoleName == roleName);
            if(role != null)
            {
                return role.AssignedUsers;
            }
            throw new ProviderException("Role does not exist!");
        }

        #endregion

        #region PRIVATE

        private void AddUserToCache(SimpleUser user)
        {
                HashSet<SimpleUser> users;
                object data = HttpContext.Current.Cache[Constants.users];
                if (data != null)
                {
                    users = (HashSet<SimpleUser>)data;
                }
                else
                {
                    users = new HashSet<SimpleUser>();
                }
                users.Add(user);
                HttpContext.Current.Cache[Constants.users] = users;
        }

        private void AddRolesToCache(IEnumerable<SimpleRole> newRoles)
        {
                HashSet<SimpleRole> roles;
                object data = HttpContext.Current.Cache[Constants.roles];
                if (data != null)
                {
                    roles = (HashSet<SimpleRole>)data;
                    roles = new HashSet<SimpleRole>(roles
                        .Union(newRoles, new RoleComparer()));
                }
                else
                {
                    roles = new HashSet<SimpleRole>(newRoles);
                }
                HttpContext.Current.Cache[Constants.roles] = roles;
        }

        private void UpdateRolesInCache(IEnumerable<SimpleRole> updatedRoles)
        {
            HashSet<SimpleRole> roles;
            object data = HttpContext.Current.Cache[Constants.roles];
            if (data != null)
            {  
                roles = (HashSet<SimpleRole>)data;

                foreach (SimpleRole updatedRole in updatedRoles)
                {
                    SimpleRole role = roles.FirstOrDefault(item => item.RoleName == updatedRole.RoleName);
                    if (role == null)
                    {
                        roles.Add(updatedRole);
                    }
                    else
                    {
                        role.AssignedUsers = role.AssignedUsers.Union(updatedRole.AssignedUsers).ToArray();
                    }
                }
            }
            else
            {
                roles = new HashSet<SimpleRole>(updatedRoles);
            }
            HttpContext.Current.Cache[Constants.roles] = roles;
        }

        private HashSet<SimpleRole> GetRoles()
        {
            object data = HttpContext.Current.Cache[Constants.roles];
            if (data == null)
            {
                SimpleRole[] roles = webServiceStore.GetRoles();
                if (roles.Length != 0)
                {
                    AddRolesToCache(roles);
                }
                else
                {
                    return new HashSet<SimpleRole>();
                }
                data = HttpContext.Current.Cache[Constants.roles];
            }
            return ((HashSet<SimpleRole>)data);
        }

        public SimpleRole[] SaveInitialData()
        {
            string[] roleNames = Constants.GetPredefinedRoles();
            SimpleRole[] roles = webServiceStore.AddRoles(roleNames);
            AddRolesToCache(roles);
            SimpleUser user = Constants.GetPredefinedAdmin();
            webServiceStore.AddUser(user.Login, user.Password);

            SimpleRole adminRole = new SimpleRole{
                RoleName = Constants.adminRole,
                AssignedUsers = new string[]{user.Login}
            };
            IEnumerable<SimpleRole> updatedRoles = webServiceStore.AddUsersToRoles(new SimpleRole[]{adminRole});
            UpdateRolesInCache(updatedRoles);
            return roles;
        }

        #endregion

    }
}