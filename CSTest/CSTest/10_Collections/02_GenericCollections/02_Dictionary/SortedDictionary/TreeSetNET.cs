using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CSTest._10_Collections._02_GenericCollections._03_HashSet;
using CSTest._30_NET_Code;

namespace CSTest._10_Collections._02_GenericCollections._02_Dictionary.SortedDictionary
{
    /// <summary>
    /// This class is intended as a helper for backwards compatibility with existing SortedDictionaries.
    /// TreeSet has been converted into SortedSetNET<T>, which will be exposed publicly. SortedDictionaries
    /// have the problem where they have already been serialized to disk as having a backing class named
    /// TreeSet. To ensure that we can read back anything that has already been written to disk, we need to
    /// make sure that we have a class named TreeSet that does everything the way it used to.
    /// 
    /// The only thing that makes it different from SortedSetNET is that it throws on duplicates
    /// </summary>
    /// <typeparam name="T"></typeparam>
#if !FEATURE_NETCORE
    [Serializable]
#endif
    internal class TreeSetNET<T> : SortedSetNET<T>
    {

        public TreeSetNET()
            : base() { }

        public TreeSetNET(IComparer<T> comparer) : base(comparer) { }

        public TreeSetNET(ICollection<T> collection) : base(collection) { }

        public TreeSetNET(ICollection<T> collection, IComparer<T> comparer) : base(collection, comparer) { }

#if !FEATURE_NETCORE
        public TreeSetNET(SerializationInfo siInfo, StreamingContext context) : base(siInfo, context) { }
#endif

        internal override bool AddIfNotPresent(T item)
        {
            bool ret = base.AddIfNotPresent(item);
            if (!ret)
            {
                ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_AddingDuplicate);
            }
            return ret;
        }

    }
}