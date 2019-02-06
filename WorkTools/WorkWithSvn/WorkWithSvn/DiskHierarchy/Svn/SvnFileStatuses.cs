using System.Collections.Generic;
using SharpSvn;
using WorkWithSvn.DiskHierarchy.Base;

namespace WorkWithSvn.DiskHierarchy.Svn
{
    public class SvnFileStatuses : RepositoryFileStatuses
    {
        ISet<SvnStatus> statusList = new HashSet<SvnStatus>();

        private bool IsModified
        {
            get
            {
                return statusList.Contains(SvnStatus.Modified)
                    || statusList.Contains(SvnStatus.Added)
                    || statusList.Contains(SvnStatus.Deleted);
            }
        }

        public override void AddModified()
        {
            statusList.Add(SvnStatus.Modified);
            statusList.Add(SvnStatus.Added);
            statusList.Add(SvnStatus.Deleted);
        }

        public void Add(SvnStatus st)
        {
            SvnStatus status = (SvnStatus)st;
            statusList.Add(status);
        }

        public bool Contains(SvnStatus st, bool isLocalStatus)
        {
            SvnStatus status = (SvnStatus)st;
            bool contains = Contains(status);
            if (!IsModified)
            {
                return contains;
            }
            else if (isLocalStatus)
            {
                return isModifiedLocal && contains;
            }
            return isModifiedOnServer && contains;
        }

        public bool Contains(SvnStatus svnStatus)
        {
            return statusList.Contains(svnStatus);
        }
    }
}
