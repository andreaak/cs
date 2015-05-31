using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.DiskHierarchy
{
    public interface IDiskItem
    {
        string FullName { get; }
        string Name { get; }
    }

    public interface IFile : IDiskItem
    {
    }
}
