using System;
using SharpSvn;
using Utils;
using WorkWithSvn.DiskHierarchy.Base;

namespace WorkWithSvn.DiskHierarchy.Svn
{
    public class SvnRepositoryHelper : DefaultRepositoryHelper, IRepositoryHelper
    {
        public static bool IsIgnoredItem(SvnStatusEventArgs file)
        {
            if (IsDirectory(file)
                && !Options.GetInstance.IsDisplayDir
                || IsUnknownFile(file))
            {
                return true;
            }
            return UTILS.IsIgnored(file.FullPath);
        }

        public static bool IsDirectory(SvnStatusEventArgs file)
        {
            return (UTILS.IsDirectory(file.FullPath) || file.NodeKind == SvnNodeKind.Directory);
        }

        private static bool IsUnknownFile(SvnStatusEventArgs file)
        {
            return file.NodeKind == SvnNodeKind.Unknown && file.LocalContentStatus != SvnStatus.NotVersioned && file.RemoteContentStatus != SvnStatus.Added;
        } 
       
        private RepositoryItem repItem;

        private SvnStatus localStatus;
        public override object LocalStatus
        {
            get
            {
                return localStatus;
            }
            set
            {
                localStatus = (SvnStatus)value;
            }
        }

        private SvnStatus remoteStatus;
        public override object RemoteStatus
        {
            get
            {
                return remoteStatus;
            }
            set
            {
                remoteStatus = (SvnStatus)value;
            }
        }

        public override bool IsModified { get { return localStatus == SvnStatus.Modified; } }

        public override bool IsAdded { get { return localStatus == SvnStatus.Added; } }

        public override bool IsDeleted
        {
            get
            {
                return localStatus == SvnStatus.Deleted
                    || remoteStatus == SvnStatus.Deleted;
            }
        }

        public override bool IsDeletedLocal { get { return localStatus == SvnStatus.Deleted; } }
        
        public override bool IsConflicted { get { return localStatus == SvnStatus.Conflicted; } }

        public override bool IsUnchanged
        {
            get
            {
                return localStatus == SvnStatus.Normal
                && remoteStatus == SvnStatus.None;
            }
        }

        public override bool IsIgnoredItem(RepositoryFileStatuses st)
        {
            SvnFileStatuses list = st as SvnFileStatuses;
            if (list == null)
            {
                throw new Exception("Wrong File Status");

            }
            bool isUnchanged = localStatus == SvnStatus.Normal && remoteStatus == SvnStatus.None;
            if (isUnchanged && list.Contains(SvnStatus.Normal)
                || !isUnchanged && (list.Contains(localStatus, true) || list.Contains(remoteStatus, false))
                || repItem.IsSwitched && list.isSwitched
                )
            {
                return false;
            }
            return true;
        }

        public override bool IsNotVersioned { get { return localStatus == SvnStatus.NotVersioned; } }

        public SvnRepositoryHelper(RepositoryItem repItem)
            : base(repItem)
        {
            this.repItem = repItem;
        }
    }
}
