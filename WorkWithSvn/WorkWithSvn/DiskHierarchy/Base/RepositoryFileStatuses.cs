using System.Collections.Generic;
using SharpSvn;

namespace WorkWithSvn.DiskHierarchy.Base
{
    public class RepositoryFileStatuses
    {
        List<object> statusList = new List<object>();
        public bool isModifiedLocal;
        public bool isModifiedOnServer;
        public bool isSwitched;

        private bool IsModified
        {
            get
            {
                return statusList.Contains("Modified")
                    || statusList.Contains("Added")
                    || statusList.Contains("Deleted");
            }
        }

        public virtual void AddModified()
        {
        }

        public void Add(object status)
        {
            statusList.Add(status);
        }

        public bool Contains(object status, bool isLocalStatus)
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

        private bool Contains(object status)
        {
            return statusList.Contains(status);
        }
    }
}
