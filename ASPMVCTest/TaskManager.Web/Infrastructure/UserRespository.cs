using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Domain;
using WebMatrix.WebData;

namespace TaskManager.Web.Infrastructure
{
    public class UserRespository : IUserProfileRepository
    {
        TasksDatabase _context;

        public UserRespository(TasksDatabase context)
        {
            _context = context;
        }

        IQueryable<UserProfile> IUserProfileRepository.All
        {
            get
            {
                return _context.UserProfiles;
            }
        }

        UserProfile IUserProfileRepository.CurrentUser
        {
            get
            {
                return _context
                    .UserProfiles
                    .Include("Tasks")
                    .Where(u => u.Id == WebSecurity.CurrentUserId)
                    .FirstOrDefault();
            }
        }

        void IUserProfileRepository.InsertOrUpdate(UserProfile user)
        {
            if (user.Id == default(int))
            {
                _context.UserProfiles.Add(user);
            }
            else
            {
                _context.Entry(user).State = System.Data.EntityState.Modified;
            }
        }

        void IUserProfileRepository.Remove(UserProfile user)
        {
            _context.Entry(user).State = System.Data.EntityState.Deleted;
        }

        void IUserProfileRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}