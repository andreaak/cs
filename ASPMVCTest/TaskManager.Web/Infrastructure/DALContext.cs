using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Domain;

namespace TaskManager.Web.Infrastructure
{
    public class DALContext : IDALContext
    {
        TasksDatabase _database;
        IUserProfileRepository _users;
        ITaskRepository _tasks;

        public DALContext()
        {
            _database = new TasksDatabase();
        }

        public IUserProfileRepository Users
        {
            get 
            {
                if (_users == null)
                {
                    _users = new UserRespository(_database);
                }
                return _users;
            }
        }

        public ITaskRepository Tasks
        {
            get 
            {
                if (_tasks == null)
                {
                    _tasks = new TaskRepository(_database);
                }
                return _tasks;
            }
        }
    }
}