using System;
using System.Collections.Generic;
using System.IO;
using SharpSvn;
using Utils.DiskHierarchy;

namespace WorkWithSvn.DiskHierarchy
{
    [Serializable]
    public class SvnDirectory : RepositoryDirectory
    {
        public SvnDirectory(DirectoryInfo directoryData)
            : base(directoryData.FullName, -1)
        {
            Helper = new SvnRepositoryHelper(this);
            foreach (DirectoryInfo dir in directoryData.GetDirectories())
            {
                if (dir.Exists && !IsIgnoredDirectories(dir.FullName))
                {
                    DirectoriesList.Add(new SvnDirectory(dir));
                }
            }
        }

        public SvnDirectory(string fullDirectoryName)
            : base(fullDirectoryName, -1)
        {
            Helper = new SvnRepositoryHelper(this);
        }

        protected override RepositoryDirectory CreateDirectory(string dirName)
        {
            return new SvnDirectory(FullName + "\\" + dirName);
        }

        protected override RepositoryFile CreateFile(string filePath)
        {
            return new SvnFile(filePath);
        }

        protected override void CleanData(RepositoryItem repItem)
        {
            base.CleanData(repItem);
            repItem.LocalStatus = SvnStatus.Zero;
        }
    }
}
