using System.Collections.Generic;
using System.Data;

namespace ReelsHelper
{
    public class Reel
    {
        public IList<int> Indexes
        {
            get;
            private set;
        }

        public int? this[int index]
        {
            get
            {
                return index < Indexes.Count ? Indexes[index] : default(int?);
            }
        }

        public Reel(IList<int> indexes)
        {
            Indexes = indexes;
        }
    }
}
