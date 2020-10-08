using System;
using Note.Domain.Common;

namespace Note.Domain.Entities
{
    public class Description
    {
        public int ID
        {
            get;
            set;
        }

        public int ParentID
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

    public class DescriptionWithStatus : Description
    {
        public DataStatus Status
        {
            get;
            set;
        }
    }

    public class DescriptionWithText : Description
    {
        public string Text
        {
            get;
            set;
        }

        public string Rtf
        {
            get;
            set;
        }
    }

}
