using System.Collections.Generic;
using SharpSvn;

namespace WorkWithSvn.DiskHierarchy
{
    public class RepoFileTypes
    {
        List<object> statusList = new List<object>();
        public bool isModifiedLocal;
        public bool isModifiedOnServer;
        public bool isSwitched;

        public virtual bool IsModified
        {
            get
            {
                return statusList.Contains(SvnStatus.Modified)
                    || statusList.Contains(SvnStatus.Added)
                    || statusList.Contains(SvnStatus.Deleted);
            }
        }

        public virtual void AddModified()
        {
        }

        public virtual void Add(object status)
        {
            statusList.Add(status);
        }

        public virtual bool Contains(object status, bool isLocalStatus)
        {
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

        public bool Contains(object status)
        {
            return statusList.Contains(status);
        }
    }
}
