using System;
using System.IO;
using Utils.DiskHierarchy;

namespace WorkWithSvn.DiskHierarchy
{
    [Serializable]
    public class RepositoryFile : RepositoryItem, IFile
    {
        public RepositoryFile(string fullName, long parentId)
            : base(fullName, Path.GetFileName(fullName), parentId)
        {
        }
    }
}
