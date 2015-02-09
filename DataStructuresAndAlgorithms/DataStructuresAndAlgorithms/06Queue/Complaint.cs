using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithms.Queue
{
    public enum ComplaintType
    {
        Noise = 0,
        Theft,
        Auto,
    }

    public class Complaint : IComparable<Complaint>
    {
        public int Id
        {
            get;
            set;
        }
        public ComplaintType Type
        {
            get;
            set;
        }

        #region IComparable<Complaint> Members

        public int CompareTo(Complaint other)
        {
            return this.Type - other.Type;
        }

        #endregion
    }
}
