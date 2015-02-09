using System.Collections.Generic;
using SharpSvn;

namespace WorkWithSvn.DiskHierarchy
{
    public class SvnFileTypes : RepoFileTypes
    {
        List<SvnStatus> statusList = new List<SvnStatus>();

        public override bool IsModified
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
            if (statusList.Contains(SvnStatus.Modified))
            {
                return;
            }
            statusList.Add(SvnStatus.Modified);
            statusList.Add(SvnStatus.Added);
            statusList.Add(SvnStatus.Deleted);
        }

        public override void Add(object st)
        {
            SvnStatus status = (SvnStatus)st;
            statusList.Add(status);
        }

        public override bool Contains(object st, bool isLocalStatus)
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
