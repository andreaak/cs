using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkWithSvn.DiskHierarchy
{
    public class DefaultRepositoryHelper : IRepositoryHelper
    {
        RepositoryItem repItem;
        
        public virtual object LocalStatus
        {
            get;
            set;
        }

        public string LocalStatusDesc
        {
            get
            {
                return Convert.ToString(LocalStatus);
            }
        }

        public virtual object RemoteStatus
        {
            get;
            set;
        }

        public string RemoteStatusDesc
        {
            get
            {
                return Convert.ToString(RemoteStatus);
            }
        }

        public virtual bool IsModified { get { return LocalStatusDesc == "Modified"; } }

        public virtual bool IsAdded { get { return LocalStatusDesc == "Added"; } }

        public virtual bool IsDeleted
        {
            get
            {
                return LocalStatusDesc == "Deleted"
                  || RemoteStatusDesc == "Deleted";
            }
        }
        
        public virtual bool IsDeletedLocal { get { return LocalStatusDesc == "Deleted" ; } }

        public virtual bool IsConflicted { get { return LocalStatusDesc == "Conflicted"; } }

        public virtual bool IsUnchanged { get { return LocalStatusDesc == "Normal"; } }

        public virtual bool IsIgnoredItem(RepoFileTypes list)
        {
            return !(repItem.IsSwitched && list.isSwitched);
        }

        public virtual bool IsNotVersioned { get { return LocalStatusDesc == "NotVersioned"; } }

        
        public DefaultRepositoryHelper(RepositoryItem repItem)
        {
            this.repItem = repItem;
        }
    }
}
