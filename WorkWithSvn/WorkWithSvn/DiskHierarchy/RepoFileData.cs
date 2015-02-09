using System;
using Utils.DiskHierarchy;

namespace WorkWithSvn.DiskHierarchy
{
    [Serializable]
    public class RepoFileData : FileData
    {
        public RepoEntityData RepoData
        {
            get;
            private set;
        }

        public RepoFileData(string fullFileName)
            : base(fullFileName, -1)
        {
            RepoData = new RepoEntityData(Data);
        }

        public static RepoFileData GetFile(FileData file)
        {
            if (file == null)
            {
                return null;
            }
            return file as RepoFileData;
        }

        public virtual RepoEntityData GetData()
        {
            return RepoData;
        }
    }
}
