using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain
{
    public interface ITaskRepository
    {
        IQueryable<Task> All { get; }
        void InsertOrUpdate(Task task);
        void Remove(Task task);
        void Save();
    }
}
