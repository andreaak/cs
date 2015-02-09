using System;
using SharpSvn;

namespace WorkWithSvn.DiskHierarchy
{
    [Serializable]
    public class SvnEntityData : RepoEntityData
    {
        public SvnStatus LocalContentStatus
        {
            get;
            set;
        }

        public SvnStatus RemoteContentStatus
        {
            get;
            set;
        }

        public SvnEntityData(RepoEntityData baseData)
            : base(baseData.Data)
        { }

        public override object LocalStatus
        {
            get { return LocalContentStatus; }
            //set;
        }

        public override string LocalStatusDesc
        {
            get { return LocalContentStatus.ToString(); }
            //set;
        }

        public override object RemoteStatus
        {
            get { return RemoteContentStatus; }
            //set;
        }

        public override string RemoteStatusDesc
        {
            get { return RemoteContentStatus.ToString(); }
            //set;
        }

        public override bool IsModified { get { return LocalContentStatus == SvnStatus.Modified; } }

        public override bool IsAdded { get { return LocalContentStatus == SvnStatus.Added; } }

        public override bool IsDeleted { get { return LocalContentStatus == SvnStatus.Deleted; } }

        public override bool IsConflicted { get { return LocalContentStatus == SvnStatus.Conflicted; } }

        public override bool IsUnchanged
        {
            get
            {
                return LocalContentStatus == SvnStatus.Normal
                && RemoteContentStatus == SvnStatus.None;
            }
        }

        public override bool IsIgnoredEntity(RepoFileTypes st)
        {
            SvnFileTypes list = st as SvnFileTypes;
            if (list == null)
            {
                throw new Exception("Wrong File Status");
            
            }
            bool isUnchanged = LocalContentStatus == SvnStatus.Normal && RemoteContentStatus == SvnStatus.None;
            if (isUnchanged && list.Contains(SvnStatus.Normal)
                || !isUnchanged && (list.Contains(LocalContentStatus, true) || list.Contains(RemoteContentStatus, false))
                || IsSwitched && list.isSwitched
                )
            {
                return false;
            }
            return true;
        }
    }
}
