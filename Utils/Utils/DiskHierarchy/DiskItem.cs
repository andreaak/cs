using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Utils.DiskHierarchy
{
    [Serializable]
    public abstract class DiskItem : IDiskItem
    {
        public static long idCount = 0;
        protected const int FILE_TYPE = 1;
        protected const int DIR_TYPE = 0;
        protected const int UNKNOWN_TYPE = -1;

        public long Id
        {
            get;
            private set;
        }

        public long ParentId
        {
            get;
            private set;
        }

        public virtual int Type
        {
            get
            {
                return UNKNOWN_TYPE;
            }
        }

        public string Name
        {
            get;
            set;
        }

        public string DirectoryName
        {
            get
            {
                return Path.GetDirectoryName(FullName);
            }
        }

        public string FullName
        {
            get;
            protected set;
        }

        public DiskItem(string fullName, string name, long parentId)
        {
            this.FullName = fullName;
            this.Name = name;
            this.Id = DiskItem.idCount++;
            this.ParentId = parentId;
        }
    }
}
