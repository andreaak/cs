using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaskManager.Domain;

namespace TaskManager.Web.Infrastructure
{
    public class TasksDatabase : DbContext 
    {
        public TasksDatabase()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}