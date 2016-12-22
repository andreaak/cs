using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ReelsHelper
{
    public class Reel
    {
        public IList<int> Indexes
        {
            get;
            set;
        }

        public int Count => Indexes.Count;

        public int? this[int index]
        {
            get
            {
                return index < Indexes.Count ? Indexes[index] : default(int?);
            }
        }

        public Reel()
        {
            Indexes = new List<int>();
        }

        public Reel(IList<int> indexes)
        {
            Indexes = indexes;
        }

        public Reel Clone()
        {
            Reel reel = new Reel(Indexes.ToArray());
            return reel;
        }

        public void Add(int icon)
        {
            Indexes.Add(icon);
        }
    }
}
