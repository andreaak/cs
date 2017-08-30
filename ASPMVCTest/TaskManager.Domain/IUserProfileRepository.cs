using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain
{
    public interface IUserProfileRepository
    {
        IQueryable<UserProfile> All { get; }
        UserProfile CurrentUser { get; }
        void InsertOrUpdate(UserProfile user);
        void Remove(UserProfile user);
        void Save();
    }
}
