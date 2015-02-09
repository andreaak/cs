using System;
using SharpSvn;

namespace WorkWithSvn.DiskHierarchy
{
    [Serializable]
    public class SvnFileData : RepoFileData
    {
        public SvnEntityData SvnData
        {
            get;
            private set;
        }

        public SvnFileData(string fullFileName)
            : base(fullFileName)
        {
            SvnData = new SvnEntityData(RepoData);
        }

        public static SvnFileData GetFile(RepoFileData file)
        {
            if (file == null)
            {
                return null;
            }
            return file as SvnFileData;
        }

        public static bool IsUnchanged(SvnStatusEventArgs le)
        {
            return le.LocalContentStatus == SvnStatus.Normal
                && le.RemoteContentStatus == SvnStatus.None;
        }

        public static bool IsUnknownFile(SvnStatusEventArgs file)
        {
            return file.NodeKind == SvnNodeKind.Unknown && file.LocalContentStatus != SvnStatus.NotVersioned && file.RemoteContentStatus != SvnStatus.Added;
        }

        public override RepoEntityData GetData()
        {
            return SvnData;
        }
    }
}
