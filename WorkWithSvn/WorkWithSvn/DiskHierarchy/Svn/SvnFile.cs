using System;
using WorkWithSvn.DiskHierarchy.Base;

namespace WorkWithSvn.DiskHierarchy.Svn
{
    [Serializable]
    public class SvnFile : RepositoryFile
    {
        public SvnFile(string fullFileName)
            : base(fullFileName, -1)
        {
            Helper = new SvnRepositoryHelper(this);
        }
    }
}
