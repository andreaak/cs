using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AuthentificationService.BusinessLogic.ServiceObjects;

namespace AuthentificationService.BusinessLogic.Store
{
    public class DBStore
    {
        #region USER

        public SimpleUser GetUserByName(string applicationId, string login)
        {
            SimpleUser user = null;
            using (DBProvider provider = new DBProvider())
            {
                provider.OpenConnection();
                string query = "SELECT UserName, Password FROM Users WHERE ApplicationId = @AppId AND UserName = @Name";
                DbParameter appParam = provider.GetParameter();
                appParam.ParameterName = "AppId";
                appParam.Value = applicationId;
                
                DbParameter nameParam = provider.GetParameter();
                nameParam.ParameterName = "Name";
                nameParam.Value = login;


                using (DbDataReader reader = provider.ExecuteReader(query, appParam, nameParam))
                {
                    if (reader.Read())
                    {
                        user = new SimpleUser
                        {
                            Login = reader.GetString(0),
                            Password = reader.GetString(1)
                        };
                    }
                }
            }
            return user;
        }

        public bool AddUser(string applicationId, string login, string password)
        {
            using (DBProvider provider = new DBProvider())
            {
                provider.OpenConnection();
                string query = "INSERT INTO Users (ApplicationId, UserName, Password) VALUES (@AppId, @Name, @Password)";
                DbParameter appParam = provider.GetParameter();
                appParam.ParameterName = "AppId";
                appParam.Value = applicationId;

                DbParameter nameParam = provider.GetParameter();
                nameParam.ParameterName = "Name";
                nameParam.Value = login;

                DbParameter passParam = provider.GetParameter();
                passParam.ParameterName = "Password";
                passParam.Value = password;

                int ret = provider.NonQueryCommand(query, appParam, nameParam, passParam);
                return ret != 0;
            }
        }

        #endregion

        #region ROLE

        public bool AddRole(string applicationId, string roleName)
        {
            using (DBProvider provider = new DBProvider())
            {
                provider.OpenConnection();
                string query = "INSERT INTO Roles (ApplicationId, RoleName) VALUES (@AppId, @Name)";
                DbParameter appParam = provider.GetParameter();
                appParam.ParameterName = "AppId";
                appParam.Value = applicationId;

                DbParameter nameParam = provider.GetParameter();
                nameParam.ParameterName = "Name";
                nameParam.Value = roleName;

                int ret = provider.NonQueryCommand(query, appParam, nameParam);
                return ret != 0;
            }
        }

        public void AddRoles(string applicationId, string[] roleNames)
        {
            using (DBProvider provider = new DBProvider())
            {
                DataTable table = new DataTable(RoleTable.name);
                
                DataColumn id = new DataColumn();
                id.DataType = typeof(int);
                id.ColumnName = RoleTable.roleIdColumn;
                id.AutoIncrement = true;
                table.Columns.Add(id);
                table.Columns.Add(new DataColumn(RoleTable.appIdColumn, typeof(string)));
                table.Columns.Add(new DataColumn(RoleTable.roleNameColumn, typeof(string)));

                foreach (string roleName in roleNames)
                {
                    DataRow row = table.NewRow();
                    row[RoleTable.appIdColumn] = applicationId;
                    row[RoleTable.roleNameColumn] = roleName;
                    table.Rows.Add(row);
                }
                table.AcceptChanges();
                provider.BatchInsert(table);
            }
        }

        public HashSet<SimpleRole> GetRoles(string applicationId)
        {
            HashSet<SimpleRole> roles = new HashSet<SimpleRole>();

            using (DBProvider provider = new DBProvider())
            {
                provider.OpenConnection();
                string query = 
                "SELECT r.RoleName, u.UserName "+
                "FROM Roles r LEFT JOIN "+
                "(Users u INNER JOIN UsersInRoles ur ON u.UserId = ur.UserId) ON r.RoleId = ur.RoleId "+
                "WHERE r.ApplicationId = @AppId";
                
                DbParameter appParam = provider.GetParameter();
                appParam.ParameterName = "AppId";
                appParam.Value = applicationId;

                using (DbDataReader reader = provider.ExecuteReader(query, appParam))
                {
                    while (reader.Read())
                    {
                        string roleName = reader.GetString(0);
                        string userName = reader.GetValue(1) as string;
                        SimpleRole role = roles.FirstOrDefault(item => item.RoleName == roleName);
                        if (role == null)
                        {
                            role = new SimpleRole
                            {
                                RoleName = roleName,
                                AssignedUsers = new HashSet<string>()
                            };
                            roles.Add(role);
                        }
                        if (!string.IsNullOrEmpty(userName))
                        {
                            role.AssignedUsers.Add(userName);
                        }
                    }
                }
            }
            return roles;
        }

        public void AddUsersToRoles(string applicationId, SimpleRole[] roles)
        {
            Dictionary<string, int> users = GetUserIds(applicationId, roles.SelectMany(item => item.AssignedUsers).ToArray());
            Dictionary<string, int> updatedRoles = GetRolesIds(applicationId, roles.Select(item => item.RoleName).ToArray());
            AddUsersToRoles(roles, users, updatedRoles);
        }

        #endregion

        #region PRIVATE

        private Dictionary<string, int> GetRolesIds(string applicationId, string[] roleNames)
        {
            string queryTemplate = "SELECT RoleId, RoleName FROM Roles WHERE ApplicationId = @AppId AND RoleName IN ({0})";
            return GetIds(applicationId, roleNames, queryTemplate);
        }

        private Dictionary<string, int> GetUserIds(string applicationId, string[] usernames)
        {
            string queryTemplate = "SELECT UserId, UserName FROM Users WHERE ApplicationId = @AppId AND UserName IN ({0})";
            return GetIds(applicationId, usernames, queryTemplate);
        }

        private Dictionary<string, int> GetIds(string applicationId, string[] roleNames, string queryTemplate)
        {
            Dictionary<string, int> ids = new Dictionary<string, int>();
            using (DBProvider provider = new DBProvider())
            {
                provider.OpenConnection();
                string[] paramNames = roleNames
                    .Select((s, i) => "@Prm" + i.ToString())
                    .ToArray();

                string inClause = string.Join(",", paramNames);
                string query = String.Format(queryTemplate, string.Join(",", paramNames));
                List<DbParameter> parameters = new List<DbParameter>();
                DbParameter appParam = provider.GetParameter();
                appParam.ParameterName = "AppId";
                appParam.Value = applicationId;
                parameters.Add(appParam);

                for (int i = 0; i < paramNames.Length; i++)
                {
                    DbParameter param = provider.GetParameter();
                    param.ParameterName = paramNames[i];
                    param.Value = roleNames[i];
                    parameters.Add(param);
                }

                using (DbDataReader reader = provider.ExecuteReader(query, parameters.ToArray()))
                {
                    while (reader.Read())
                    {
                        ids.Add(reader.GetString(1), reader.GetInt32(0));
                    }
                }
            }
            return ids;
        }

        private void AddUsersToRoles(SimpleRole[] roles, Dictionary<string, int> usersIds, Dictionary<string, int> rolesIds)
        {
            using (DBProvider provider = new DBProvider())
            {
                DataTable table = new DataTable(UsersInRolesTable.name);
                table.Columns.Add(new DataColumn(UsersInRolesTable.userIdColumn, typeof(string)));
                table.Columns.Add(new DataColumn(UsersInRolesTable.roleIdColumn, typeof(string)));

                foreach (SimpleRole role in roles)
                {
                    foreach (string userName in role.AssignedUsers)
                    {
                        DataRow row = table.NewRow();
                        row[UsersInRolesTable.userIdColumn] = usersIds[userName];
                        row[UsersInRolesTable.roleIdColumn] = rolesIds[role.RoleName];
                        table.Rows.Add(row);
                    }
                }
                table.AcceptChanges();
                provider.BatchInsert(table);
            }
        }
        
        #endregion

    }
}