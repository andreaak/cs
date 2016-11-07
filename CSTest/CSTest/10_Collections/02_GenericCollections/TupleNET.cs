using CSTest._30_NET_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CSTest._10_Collections._02_GenericCollections
{
    internal interface ITuple
    {
        int Size { get; }

        string ToString(StringBuilder sb);

        int GetHashCode(IEqualityComparer comparer);
    }

    [Serializable]
    public class TupleNET<T1, T2> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
    {

        private readonly T1 m_Item1;
        private readonly T2 m_Item2;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }

        public TupleNET(T1 item1, T2 item2)
        {
            m_Item1 = item1;
            m_Item2 = item2;
        }

        public override Boolean Equals(Object obj)
        {
            return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<Object>.Default); ;
        }

        Boolean IStructuralEquatable.Equals(Object other, IEqualityComparer comparer)
        {
            if (other == null) return false;

            TupleNET<T1, T2> objTuple = other as TupleNET<T1, T2>;

            if (objTuple == null)
            {
                return false;
            }

            return comparer.Equals(m_Item1, objTuple.m_Item1) && comparer.Equals(m_Item2, objTuple.m_Item2);
        }

        Int32 IComparable.CompareTo(Object obj)
        {
            return ((IStructuralComparable)this).CompareTo(obj, Comparer<Object>.Default);
        }

        Int32 IStructuralComparable.CompareTo(Object other, IComparer comparer)
        {
            if (other == null) return 1;

            TupleNET<T1, T2> objTuple = other as TupleNET<T1, T2>;

            if (objTuple == null)
            {
                throw new ArgumentException(EnvironmentNET.GetResourceString("ArgumentException_TupleIncorrectType", this.GetType().ToString()), "other");
            }

            int c = 0;

            c = comparer.Compare(m_Item1, objTuple.m_Item1);

            if (c != 0) return c;

            return comparer.Compare(m_Item2, objTuple.m_Item2);
        }

        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<Object>.Default);
        }

        Int32 IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return TupleNET.CombineHashCodes(comparer.GetHashCode(m_Item1), comparer.GetHashCode(m_Item2));
        }

        Int32 ITuple.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)this).GetHashCode(comparer);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append(m_Item1);
            sb.Append(", ");
            sb.Append(m_Item2);
            sb.Append(")");
            return sb.ToString();
        }

        int ITuple.Size
        {
            get
            {
                return 2;
            }
        }
    }

    public static class TupleNET
    {
        internal static int CombineHashCodes(int h1, int h2)
        {
            return (((h1 << 5) + h1) ^ h2);
        }
    }
}
