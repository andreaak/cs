using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.DiskHierarchy
{
    public interface IDirectory : IDiskItem
    {
        IEnumerable<IDirectory> DirectoriesList
        {
            get;
        }

        IEnumerable<IFile> FilesList
        {
            get;
        }

        IDirectory CreateAndAddDirectory(string fullName);

        void RemoveDirectory(string fullName);

        IFile CreateAndAddFile(string fileFullName);

        void RemoveFile(string fileFullName);
    }
}
