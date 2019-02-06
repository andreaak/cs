using System;
using System.IO;
using Utils.DiskHierarchy;

namespace WorkWithSvn.DiskHierarchy.Base
{
    [Serializable]
    public class RepositoryItem : DiskItem
    {
        public IRepositoryHelper helper;
        public IRepositoryHelper Helper
        {
            get 
            { 
                if(helper == null)
                {
                    helper = new DefaultRepositoryHelper(this);
                }
                return helper; 
            }
            set
            {
                helper = value; 
            }
        }
        
        public string Revision
        {
            get;
            set;
        }

        public string ChangeList
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public bool IsSwitched
        {
            get;
            set;
        }

        public object LocalStatus
        {
            get
            {
                return Helper.LocalStatus;
            }
            set
            {
                Helper.LocalStatus = value;
            }
        }

        public string LocalStatusDesc
        {
            get
            {
                return Helper.LocalStatusDesc;
            }
        }

        public object RemoteStatus
        {
            get
            {
                return Helper.RemoteStatus;
            }
            set
            {
                Helper.RemoteStatus = value;
            }
        }

        public string RemoteStatusDesc
        {
            get
            {
                return Helper.RemoteStatusDesc;
            }
        }

        public bool IsModified { get { return Helper.IsModified; } }

        public bool IsAdded { get { return Helper.IsAdded; } }

        public bool IsDeleted { get { return Helper.IsDeleted; } }

        public bool IsDeletedLocal { get { return Helper.IsDeletedLocal; } }

        public bool IsConflicted { get { return Helper.IsConflicted; } }

        public bool IsUnchanged { get { return Helper.IsUnchanged; } }
        
        public bool IsIgnoredItem(RepositoryFileStatuses list)
        {
            return Helper.IsIgnoredItem(list);
        }

        public bool IsNotVersioned { get { return Helper.IsNotVersioned; } }

        public RepositoryItem(string fullName, string name, long parentId)
            : base(fullName, name, parentId)
        {

        }

        public void Rename(string name)
        {
            Name = name;
            FullName = Path.GetDirectoryName(FullName) + Path.DirectorySeparatorChar + name;
        }
    }
}
