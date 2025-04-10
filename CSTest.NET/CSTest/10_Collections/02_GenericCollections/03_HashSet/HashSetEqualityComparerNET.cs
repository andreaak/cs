using System;
using System.Collections.Generic;

namespace CSTest._10_Collections._02_GenericCollections._03_HashSet
{
    /// <summary>
    /// Equality comparer for hashsets of hashsets
    /// </summary>
    /// <typeparam name="T"></typeparam>
#if !FEATURE_NETCORE
    [Serializable()]
#endif
    internal class HashSetEqualityComparerNET<T> : IEqualityComparer<HashSetNET<T>>
    {

        private IEqualityComparer<T> m_comparer;

        public HashSetEqualityComparerNET()
        {
            m_comparer = EqualityComparer<T>.Default;
        }

        public HashSetEqualityComparerNET(IEqualityComparer<T> comparer)
        {
            if (comparer == null)
            {
                m_comparer = EqualityComparer<T>.Default;
            }
            else
            {
                m_comparer = comparer;
            }
        }

        // using m_comparer to keep equals properties in tact; don't want to choose one of the comparers
        public bool Equals(HashSetNET<T> x, HashSetNET<T> y)
        {
            return HashSetNET<T>.HashSetEquals(x, y, m_comparer);
        }

        public int GetHashCode(HashSetNET<T> obj)
        {
            int hashCode = 0;
            if (obj != null)
            {
                foreach (T t in obj)
                {
                    hashCode = hashCode ^ (m_comparer.GetHashCode(t) & 0x7FFFFFFF);
                }
            } // else returns hashcode of 0 for null hashsets
            return hashCode;
        }

        // Equals method for the comparer itself. 
        public override bool Equals(Object obj)
        {
            HashSetEqualityComparerNET<T> comparer = obj as HashSetEqualityComparerNET<T>;
            if (comparer == null)
            {
                return false;
            }
            return (this.m_comparer == comparer.m_comparer);
        }

        public override int GetHashCode()
        {
            return m_comparer.GetHashCode();
        }
    }
}