using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain
{
    public interface IDALContext
    {
        IUserProfileRepository Users { get; }
        ITaskRepository Tasks { get; }
    }
}
