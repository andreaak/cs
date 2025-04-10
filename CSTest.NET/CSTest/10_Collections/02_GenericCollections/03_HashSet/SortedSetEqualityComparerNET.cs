using System;
using System.Collections.Generic;

namespace CSTest._10_Collections._02_GenericCollections._03_HashSet
{
    /// <summary>
    /// A class that generates an IEqualityComparer for this SortedSet. Requires that the definition of
    /// equality defined by the IComparer for this SortedSet be consistent with the default IEqualityComparer
    /// for the type T. If not, such an IEqualityComparer should be provided through the constructor.
    /// </summary>    
    internal class SortedSetEqualityComparerNET<T> : IEqualityComparer<SortedSetNET<T>>
    {
        private IComparer<T> comparer;
        private IEqualityComparer<T> e_comparer;

        public SortedSetEqualityComparerNET() : this(null, null) { }

        public SortedSetEqualityComparerNET(IComparer<T> comparer) : this(comparer, null) { }

        public SortedSetEqualityComparerNET(IEqualityComparer<T> memberEqualityComparer) : this(null, memberEqualityComparer) { }

        /// <summary>
        /// Create a new SetEqualityComparer, given a comparer for member order and another for member equality (these
        /// must be consistent in their definition of equality)
        /// </summary>        
        public SortedSetEqualityComparerNET(IComparer<T> comparer, IEqualityComparer<T> memberEqualityComparer)
        {
            if (comparer == null)
                this.comparer = Comparer<T>.Default;
            else
                this.comparer = comparer;
            if (memberEqualityComparer == null)
                e_comparer = EqualityComparer<T>.Default;
            else
                e_comparer = memberEqualityComparer;
        }


        // using comparer to keep equals properties in tact; don't want to choose one of the comparers
        public bool Equals(SortedSetNET<T> x, SortedSetNET<T> y)
        {
            return SortedSetNET<T>.SortedSetEquals(x, y, comparer);
        }
        //IMPORTANT: this part uses the fact that GetHashCode() is consistent with the notion of equality in
        //the set
        public int GetHashCode(SortedSetNET<T> obj)
        {
            int hashCode = 0;
            if (obj != null)
            {
                foreach (T t in obj)
                {
                    hashCode = hashCode ^ (e_comparer.GetHashCode(t) & 0x7FFFFFFF);
                }
            } // else returns hashcode of 0 for null HashSets
            return hashCode;
        }

        // Equals method for the comparer itself. 
        public override bool Equals(Object obj)
        {
            SortedSetEqualityComparerNET<T> comparer = obj as SortedSetEqualityComparerNET<T>;
            if (comparer == null)
            {
                return false;
            }
            return (this.comparer == comparer.comparer);
        }

        public override int GetHashCode()
        {
            return comparer.GetHashCode() ^ e_comparer.GetHashCode();
        }
    }
}