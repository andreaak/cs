using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthentificationService.BusinessLogic
{
    public class Constants
    {
        public const string connectionString = "Authentification";
    }

    public class UserTable
    {
        public const string name = "Users";
        public const string userIdColumn = "UserId";
        public const string appIdColumn = "ApplicationId";
        public const string userNameColumn = "UserName";
    }

    public class RoleTable
    {
        public const string name = "Roles";
        public const string roleIdColumn = "RoleId";
        public const string appIdColumn = "ApplicationId";
        public const string roleNameColumn = "RoleName";
    }

    public class UsersInRolesTable
    {
        public const string name = "UsersInRoles";
        public const string userIdColumn = "UserId";
        public const string roleIdColumn = "RoleId";
    }
}