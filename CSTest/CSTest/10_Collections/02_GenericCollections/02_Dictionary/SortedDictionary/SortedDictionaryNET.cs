using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using CSTest._30_NET_Code;

namespace CSTest._10_Collections._02_GenericCollections._02_Dictionary.SortedDictionary
{
#if !FEATURE_NETCORE
    [Serializable]
#endif
    //[DebuggerTypeProxy(typeof(System_DictionaryDebugView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    public class SortedDictionaryNET<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>
    {
#if !FEATURE_NETCORE
        [NonSerialized]
#endif
        private KeyCollection keys;

#if !FEATURE_NETCORE
        [NonSerialized]
#endif
        private ValueCollection values;

        private TreeSetNET<KeyValuePair<TKey, TValue>> _set;

        public SortedDictionaryNET() : this((IComparer<TKey>)null)
        {
        }

        public SortedDictionaryNET(IDictionary<TKey, TValue> dictionary) : this(dictionary, null)
        {
        }

        public SortedDictionaryNET(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
        {
            if (dictionary == null)
            {
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.dictionary);
            }

            _set = new TreeSetNET<KeyValuePair<TKey, TValue>>(new KeyValuePairComparer(comparer));

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                _set.Add(pair);
            }
        }

        public SortedDictionaryNET(IComparer<TKey> comparer)
        {
            _set = new TreeSetNET<KeyValuePair<TKey, TValue>>(new KeyValuePairComparer(comparer));
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            _set.Add(keyValuePair);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            TreeSetNET<KeyValuePair<TKey, TValue>>.Node node = _set.FindNode(keyValuePair);
            if (node == null)
            {
                return false;
            }

            if (keyValuePair.Value == null)
            {
                return node.Item.Value == null;
            }
            else
            {
                return EqualityComparer<TValue>.Default.Equals(node.Item.Value, keyValuePair.Value);
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            TreeSetNET<KeyValuePair<TKey, TValue>>.Node node = _set.FindNode(keyValuePair);
            if (node == null)
            {
                return false;
            }

            if (EqualityComparer<TValue>.Default.Equals(node.Item.Value, keyValuePair.Value))
            {
                _set.Remove(keyValuePair);
                return true;
            }
            return false;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                {
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
                }

                TreeSetNET<KeyValuePair<TKey, TValue>>.Node node = _set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
                if (node == null)
                {
                    ThrowHelperNET.ThrowKeyNotFoundException();
                }

                return node.Item.Value;
            }
            set
            {
                if (key == null)
                {
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
                }

                TreeSetNET<KeyValuePair<TKey, TValue>>.Node node = _set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
                if (node == null)
                {
                    _set.Add(new KeyValuePair<TKey, TValue>(key, value));
                }
                else
                {
                    node.Item = new KeyValuePair<TKey, TValue>(node.Item.Key, value);
                    _set.UpdateVersion();
                }
            }
        }

        public int Count
        {
            get
            {
                return _set.Count;
            }
        }

        public IComparer<TKey> Comparer
        {
            get
            {
                return ((KeyValuePairComparer)_set.Comparer).keyComparer;
            }
        }

        public KeyCollection Keys
        {
            get
            {
                if (keys == null) keys = new KeyCollection(this);
                return keys;
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }

        public ValueCollection Values
        {
            get
            {
                if (values == null) values = new ValueCollection(this);
                return values;
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values;
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            }
            _set.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Clear()
        {
            _set.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
            {
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            }

            return _set.Contains(new KeyValuePair<TKey, TValue>(key, default(TValue)));
        }

        public bool ContainsValue(TValue value)
        {
            bool found = false;
            if (value == null)
            {
                _set.InOrderTreeWalk(delegate (TreeSetNET<KeyValuePair<TKey, TValue>>.Node node)
                {
                    if (node.Item.Value == null)
                    {
                        found = true;
                        return false;  // stop the walk
                    }
                    return true;
                });

            }
            else
            {
                EqualityComparer<TValue> valueComparer = EqualityComparer<TValue>.Default;
                _set.InOrderTreeWalk(delegate (TreeSetNET<KeyValuePair<TKey, TValue>>.Node node)
                {
                    if (valueComparer.Equals(node.Item.Value, value))
                    {
                        found = true;
                        return false;  // stop the walk
                    }
                    return true;
                });
            }
            return found;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            _set.CopyTo(array, index);
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this, Enumerator.KeyValuePair);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new Enumerator(this, Enumerator.KeyValuePair);
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            }

            return _set.Remove(new KeyValuePair<TKey, TValue>(key, default(TValue)));
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
            {
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            }

            TreeSetNET<KeyValuePair<TKey, TValue>>.Node node = _set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
            if (node == null)
            {
                value = default(TValue);
                return false;
            }
            value = node.Item.Value;
            return true;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_set).CopyTo(array, index);
        }

        bool IDictionary.IsFixedSize
        {
            get { return false; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return false; }
        }

        ICollection IDictionary.Keys
        {
            get { return (ICollection)Keys; }
        }

        ICollection IDictionary.Values
        {
            get { return (ICollection)Values; }
        }

        object IDictionary.this[object key]
        {
            get
            {
                if (IsCompatibleKey(key))
                {
                    TValue value;
                    if (TryGetValue((TKey)key, out value))
                    {
                        return value;
                    }
                }

                return null;
            }
            set
            {
                if (key == null)
                {
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
                }

                ThrowHelperNET.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgumentNET.value);

                try
                {
                    TKey tempKey = (TKey)key;
                    try
                    {
                        this[tempKey] = (TValue)value;
                    }
                    catch (InvalidCastException)
                    {
                        ThrowHelperNET.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                    }
                }
                catch (InvalidCastException)
                {
                    ThrowHelperNET.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
                }
            }
        }

        void IDictionary.Add(object key, object value)
        {
            if (key == null)
            {
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            }

            ThrowHelperNET.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgumentNET.value);

            try
            {
                TKey tempKey = (TKey)key;

                try
                {
                    Add(tempKey, (TValue)value);
                }
                catch (InvalidCastException)
                {
                    ThrowHelperNET.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                }
            }
            catch (InvalidCastException)
            {
                ThrowHelperNET.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
            }
        }

        bool IDictionary.Contains(object key)
        {
            if (IsCompatibleKey(key))
            {
                return ContainsKey((TKey)key);
            }
            return false;
        }

        private static bool IsCompatibleKey(object key)
        {
            if (key == null)
            {
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            }

            return (key is TKey);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new Enumerator(this, Enumerator.DictEntry);
        }

        void IDictionary.Remove(object key)
        {
            if (IsCompatibleKey(key))
            {
                Remove((TKey)key);
            }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection)_set).SyncRoot; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this, Enumerator.KeyValuePair);
        }

        [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "not an expected scenario")]
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDictionaryEnumerator
        {
            private TreeSetNET<KeyValuePair<TKey, TValue>>.Enumerator treeEnum;
            private int getEnumeratorRetType;  // What should Enumerator.Current return?

            internal const int KeyValuePair = 1;
            internal const int DictEntry = 2;

            internal Enumerator(SortedDictionaryNET<TKey, TValue> dictionary, int getEnumeratorRetType)
            {
                treeEnum = dictionary._set.GetEnumerator();
                this.getEnumeratorRetType = getEnumeratorRetType;
            }

            public bool MoveNext()
            {
                return treeEnum.MoveNext();
            }

            public void Dispose()
            {
                treeEnum.Dispose();
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    return treeEnum.Current;
                }
            }

            internal bool NotStartedOrEnded
            {
                get
                {
                    return treeEnum.NotStartedOrEnded;
                }
            }

            internal void Reset()
            {
                treeEnum.Reset();
            }


            void IEnumerator.Reset()
            {
                treeEnum.Reset();
            }

            object IEnumerator.Current
            {
                get
                {
                    if (NotStartedOrEnded)
                    {
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                    }

                    if (getEnumeratorRetType == DictEntry)
                    {
                        return new DictionaryEntry(Current.Key, Current.Value);
                    }
                    else
                    {
                        return new KeyValuePair<TKey, TValue>(Current.Key, Current.Value);
                    }

                }
            }

            object IDictionaryEnumerator.Key
            {
                get
                {
                    if (NotStartedOrEnded)
                    {
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                    }

                    return Current.Key;
                }
            }

            object IDictionaryEnumerator.Value
            {
                get
                {
                    if (NotStartedOrEnded)
                    {
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                    }

                    return Current.Value;
                }
            }

            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    if (NotStartedOrEnded)
                    {
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                    }

                    return new DictionaryEntry(Current.Key, Current.Value);
                }
            }
        }

        // [DebuggerTypeProxy(typeof(System_DictionaryKeyCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
#if !FEATURE_NETCORE
        [Serializable]
#endif
        public sealed class KeyCollection : ICollection<TKey>, ICollection, IReadOnlyCollection<TKey>
        {
            private SortedDictionaryNET<TKey, TValue> dictionary;

            public KeyCollection(SortedDictionaryNET<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                {
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.dictionary);
                }
                this.dictionary = dictionary;
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            public void CopyTo(TKey[] array, int index)
            {
                if (array == null)
                {
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.array);
                }

                if (index < 0)
                {
                    ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.index);
                }

                if (array.Length - index < Count)
                {
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_ArrayPlusOffTooSmall);
                }

                dictionary._set.InOrderTreeWalk(delegate (TreeSetNET<KeyValuePair<TKey, TValue>>.Node node) { array[index++] = node.Item.Key; return true; });
            }

            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                {
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.array);
                }

                if (array.Rank != 1)
                {
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_RankMultiDimNotSupported);
                }

                if (array.GetLowerBound(0) != 0)
                {
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_NonZeroLowerBound);
                }

                if (index < 0)
                {
                    ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.arrayIndex, ExceptionResourceNET.ArgumentOutOfRange_NeedNonNegNum);
                }

                if (array.Length - index < dictionary.Count)
                {
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_ArrayPlusOffTooSmall);
                }

                TKey[] keys = array as TKey[];
                if (keys != null)
                {
                    CopyTo(keys, index);
                }
                else
                {
                    object[] objects = (object[])array;
                    if (objects == null)
                    {
                        ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_InvalidArrayType);
                    }

                    try
                    {
                        dictionary._set.InOrderTreeWalk(delegate (TreeSetNET<KeyValuePair<TKey, TValue>>.Node node) { objects[index++] = node.Item.Key; return true; });
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_InvalidArrayType);
                    }
                }
            }

            public int Count
            {
                get { return dictionary.Count; }
            }

            bool ICollection<TKey>.IsReadOnly
            {
                get { return true; }
            }

            void ICollection<TKey>.Add(TKey item)
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_KeyCollectionSet);
            }

            void ICollection<TKey>.Clear()
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_KeyCollectionSet);
            }

            bool ICollection<TKey>.Contains(TKey item)
            {
                return dictionary.ContainsKey(item);
            }

            bool ICollection<TKey>.Remove(TKey item)
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_KeyCollectionSet);
                return false;
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            Object ICollection.SyncRoot
            {
                get { return ((ICollection)dictionary).SyncRoot; }
            }

            [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "not an expected scenario")]
            public struct Enumerator : IEnumerator<TKey>, IEnumerator
            {
                private SortedDictionaryNET<TKey, TValue>.Enumerator dictEnum;

                internal Enumerator(SortedDictionaryNET<TKey, TValue> dictionary)
                {
                    dictEnum = dictionary.GetEnumerator();
                }

                public void Dispose()
                {
                    dictEnum.Dispose();
                }

                public bool MoveNext()
                {
                    return dictEnum.MoveNext();
                }

                public TKey Current
                {
                    get
                    {
                        return dictEnum.Current.Key;
                    }
                }

                object IEnumerator.Current
                {
                    get
                    {
                        if (dictEnum.NotStartedOrEnded)
                        {
                            ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                        }

                        return Current;
                    }
                }

                void IEnumerator.Reset()
                {
                    dictEnum.Reset();
                }

            }
        }

        //[DebuggerTypeProxy(typeof(System_DictionaryValueCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
#if !FEATURE_NETCORE
        [Serializable]
#endif
        public sealed class ValueCollection : ICollection<TValue>, ICollection, IReadOnlyCollection<TValue>
        {
            private SortedDictionaryNET<TKey, TValue> dictionary;

            public ValueCollection(SortedDictionaryNET<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                {
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.dictionary);
                }
                this.dictionary = dictionary;
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            public void CopyTo(TValue[] array, int index)
            {
                if (array == null)
                {
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.array);
                }

                if (index < 0)
                {
                    ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.index);
                }

                if (array.Length - index < Count)
                {
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_ArrayPlusOffTooSmall);
                }

                dictionary._set.InOrderTreeWalk(delegate (TreeSetNET<KeyValuePair<TKey, TValue>>.Node node) { array[index++] = node.Item.Value; return true; });
            }

            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                {
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.array);
                }

                if (array.Rank != 1)
                {
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_RankMultiDimNotSupported);
                }

                if (array.GetLowerBound(0) != 0)
                {
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_NonZeroLowerBound);
                }

                if (index < 0)
                {
                    ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.arrayIndex, ExceptionResourceNET.ArgumentOutOfRange_NeedNonNegNum);
                }

                if (array.Length - index < dictionary.Count)
                {
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_ArrayPlusOffTooSmall);
                }

                TValue[] values = array as TValue[];
                if (values != null)
                {
                    CopyTo(values, index);
                }
                else
                {
                    object[] objects = (object[])array;
                    if (objects == null)
                    {
                        ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_InvalidArrayType);
                    }

                    try
                    {
                        dictionary._set.InOrderTreeWalk(delegate (TreeSetNET<KeyValuePair<TKey, TValue>>.Node node) { objects[index++] = node.Item.Value; return true; });
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_InvalidArrayType);
                    }
                }
            }

            public int Count
            {
                get { return dictionary.Count; }
            }

            bool ICollection<TValue>.IsReadOnly
            {
                get { return true; }
            }

            void ICollection<TValue>.Add(TValue item)
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_ValueCollectionSet);
            }

            void ICollection<TValue>.Clear()
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_ValueCollectionSet);
            }

            bool ICollection<TValue>.Contains(TValue item)
            {
                return dictionary.ContainsValue(item);
            }

            bool ICollection<TValue>.Remove(TValue item)
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_ValueCollectionSet);
                return false;
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            Object ICollection.SyncRoot
            {
                get { return ((ICollection)dictionary).SyncRoot; }
            }

            [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "not an expected scenario")]
            public struct Enumerator : IEnumerator<TValue>, IEnumerator
            {
                private SortedDictionaryNET<TKey, TValue>.Enumerator dictEnum;

                internal Enumerator(SortedDictionaryNET<TKey, TValue> dictionary)
                {
                    dictEnum = dictionary.GetEnumerator();
                }

                public void Dispose()
                {
                    dictEnum.Dispose();
                }

                public bool MoveNext()
                {
                    return dictEnum.MoveNext();
                }

                public TValue Current
                {
                    get
                    {
                        return dictEnum.Current.Value;
                    }
                }

                object IEnumerator.Current
                {
                    get
                    {
                        if (dictEnum.NotStartedOrEnded)
                        {
                            ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                        }

                        return Current;
                    }
                }

                void IEnumerator.Reset()
                {
                    dictEnum.Reset();
                }
            }
        }

#if !FEATURE_NETCORE
        [Serializable]
#endif
        internal class KeyValuePairComparer : Comparer<KeyValuePair<TKey, TValue>>
        {
            internal IComparer<TKey> keyComparer;

            public KeyValuePairComparer(IComparer<TKey> keyComparer)
            {
                if (keyComparer == null)
                {
                    this.keyComparer = Comparer<TKey>.Default;
                }
                else
                {
                    this.keyComparer = keyComparer;
                }
            }

            public override int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            {
                return keyComparer.Compare(x.Key, y.Key);
            }
        }
    }
}

