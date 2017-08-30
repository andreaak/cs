using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain;

namespace TaskManager.Web.Tests.Infrastructure
{
    class TestDALContext : IDALContext
    {
        private static IUserProfileRepository _users = new TestUserRepository();
        private static ITaskRepository _tasks = new TestTaskRepository();

        public IUserProfileRepository Users
        {
            get { return _users; }
        }

        public ITaskRepository Tasks
        {
            get { return _tasks; }
        }
    }
}
