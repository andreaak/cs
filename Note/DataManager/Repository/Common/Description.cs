using System;

namespace DataManager.Repository.Common
{
    public class Description
    {
        public long ID
        {
            get;
            set;
        }

        public long ParentID
        {
            get;
            set;
        }

        public DateTime ModDate
        {
            get;
            set;
        }

        public int OrderPosition
        {
            get;
            set;
        }

        public DataTypes Type
        {
            get;
            set;
        }

        public string EditValue
        {
            get;
            set;
        }
    }
}
