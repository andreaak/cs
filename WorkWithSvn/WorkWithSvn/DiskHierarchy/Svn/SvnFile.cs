using System;
using SharpSvn;

namespace WorkWithSvn.DiskHierarchy
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
