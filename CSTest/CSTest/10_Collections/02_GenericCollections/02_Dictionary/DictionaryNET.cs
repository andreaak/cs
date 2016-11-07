// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.Dictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: B85DA6F1-FFD3-4C95-AF10-155C235A661D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using CSTest._30_NET_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace CSTest._10_Collections._02_GenericCollections._02_Dictionary
{
    /// <summary>
    /// Represents a collection of keys and values.To browse the .NET Framework source code for this type, see the Reference Source.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam><typeparam name="TValue">The type of the values in the dictionary.</typeparam><filterpriority>1</filterpriority>
    //[DebuggerTypeProxy(typeof(Mscorlib_DictionaryDebugView<,>))]
    //[DebuggerDisplay("Count = {Count}")]
    //[ComVisible(false)]
    ////[__DynamicallyInvokable]
    [Serializable]
    public class DictionaryNET<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection
#if CS5
        , IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>> 
#endif
        , ISerializable, IDeserializationCallback
    {
        public int[] buckets;
        public DictionaryNET<TKey, TValue>.Entry[] entries;
        private int count;
        private int version;
        private int freeList;
        private int freeCount;
        private IEqualityComparer<TKey> comparer;
        private DictionaryNET<TKey, TValue>.KeyCollection keys;
        private DictionaryNET<TKey, TValue>.ValueCollection values;
        private object _syncRoot;
        private const string VersionName = "Version";
        private const string HashSizeName = "HashSize";
        private const string KeyValuePairsName = "KeyValuePairs";
        private const string ComparerName = "Comparer";

        /// <summary>
        /// Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> that is used to determine equality of keys for the dictionary.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> generic interface implementation that is used to determine equality of keys for the current <see cref="T:System.Collections.Generic.Dictionary`2"/> and to provide hash values for the keys.
        /// </returns>
        //[__DynamicallyInvokable]
        public IEqualityComparer<TKey> Comparer
        {
            //[__DynamicallyInvokable]
            get
            {
                return this.comparer;
            }
        }

        /// <summary>
        /// Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </returns>
        //[__DynamicallyInvokable]
        public int Count
        {
            //[__DynamicallyInvokable]
            get
            {
                return this.count - this.freeCount;
            }
        }

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/> containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </returns>
        //[__DynamicallyInvokable]
        public DictionaryNET<TKey, TValue>.KeyCollection Keys
        {
            //[__DynamicallyInvokable]
            get
            {
                if (this.keys == null)
                    this.keys = new DictionaryNET<TKey, TValue>.KeyCollection(this);
                return this.keys;
            }
        }

        //[__DynamicallyInvokable]
        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            //[__DynamicallyInvokable]
            get
            {
                if (this.keys == null)
                    this.keys = new DictionaryNET<TKey, TValue>.KeyCollection(this);
                return (ICollection<TKey>)this.keys;
            }
        }
#if CS5
        //[__DynamicallyInvokable]
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            //[__DynamicallyInvokable]
            get
            {
                if (this.keys == null)
                    this.keys = new DictionaryNET<TKey, TValue>.KeyCollection(this);
                return (IEnumerable<TKey>)this.keys;
            }
        }
#endif
        /// <summary>
        /// Gets a collection containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/> containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </returns>
        //[__DynamicallyInvokable]
        public DictionaryNET<TKey, TValue>.ValueCollection Values
        {
            //[__DynamicallyInvokable]
            get
            {
                if (this.values == null)
                    this.values = new DictionaryNET<TKey, TValue>.ValueCollection(this);
                return this.values;
            }
        }

        //[__DynamicallyInvokable]
        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            //[__DynamicallyInvokable]
            get
            {
                if (this.values == null)
                    this.values = new DictionaryNET<TKey, TValue>.ValueCollection(this);
                return (ICollection<TValue>)this.values;
            }
        }
#if CS5
        //[__DynamicallyInvokable]
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            //[__DynamicallyInvokable]
            get
            {
                if (this.values == null)
                    this.values = new DictionaryNET<TKey, TValue>.ValueCollection(this);
                return (IEnumerable<TValue>)this.values;
            }
        }
#endif
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// 
        /// <returns>
        /// The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException"/>, and a set operation creates a new element with the specified key.
        /// </returns>
        /// <param name="key">The key of the value to get or set.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception><exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key"/> does not exist in the collection.</exception>
        //[__DynamicallyInvokable]
        public TValue this[TKey key]
        {
            //[__DynamicallyInvokable]
            get
            {
                int entry = this.FindEntry(key);
                if (entry >= 0)
                    return this.entries[entry].value;
                ThrowHelperNET.ThrowKeyNotFoundException();
                return default(TValue);
            }
            //[__DynamicallyInvokable]
            set
            {
                this.Insert(key, value, false);
            }
        }

        //[__DynamicallyInvokable]
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            //[__DynamicallyInvokable]
            get
            {
                return false;
            }
        }

        //[__DynamicallyInvokable]
        bool ICollection.IsSynchronized
        {
            //[__DynamicallyInvokable]
            get
            {
                return false;
            }
        }

        //[__DynamicallyInvokable]
        object ICollection.SyncRoot
        {
            //[__DynamicallyInvokable]
            get
            {
                if (this._syncRoot == null)
                    Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object)null);
                return this._syncRoot;
            }
        }

        //[__DynamicallyInvokable]
        bool IDictionary.IsFixedSize
        {
            //[__DynamicallyInvokable]
            get
            {
                return false;
            }
        }

        //[__DynamicallyInvokable]
        bool IDictionary.IsReadOnly
        {
            //[__DynamicallyInvokable]
            get
            {
                return false;
            }
        }

        //[__DynamicallyInvokable]
        ICollection IDictionary.Keys
        {
            //[__DynamicallyInvokable]
            get
            {
                return (ICollection)this.Keys;
            }
        }

        //[__DynamicallyInvokable]
        ICollection IDictionary.Values
        {
            //[__DynamicallyInvokable]
            get
            {
                return (ICollection)this.Values;
            }
        }

        //[__DynamicallyInvokable]
        object IDictionary.this[object key]
        {
            //[__DynamicallyInvokable]
            get
            {
                if (DictionaryNET<TKey, TValue>.IsCompatibleKey(key))
                {
                    int entry = this.FindEntry((TKey)key);
                    if (entry >= 0)
                        return (object)this.entries[entry].value;
                }
                return (object)null;
            }
            //[__DynamicallyInvokable]
            set
            {
                if (key == null)
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
                ThrowHelperNET.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgumentNET.value);
                try
                {
                    TKey index = (TKey)key;
                    try
                    {
                        this[index] = (TValue)value;
                    }
                    catch (InvalidCastException ex)
                    {
                        ThrowHelperNET.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                    }
                }
                catch (InvalidCastException ex)
                {
                    ThrowHelperNET.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        //[__DynamicallyInvokable]
        public DictionaryNET()
          : this(0, (IEqualityComparer<TKey>)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2"/> can contain.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0.</exception>
        //[__DynamicallyInvokable]
        public DictionaryNET(int capacity)
          : this(capacity, (IEqualityComparer<TKey>)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"/> for the type of the key.</param>
        //[__DynamicallyInvokable]
        public DictionaryNET(IEqualityComparer<TKey> comparer)
          : this(0, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"/>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2"/> can contain.</param><param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"/> for the type of the key.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0.</exception>
        //[__DynamicallyInvokable]
        public DictionaryNET(int capacity, IEqualityComparer<TKey> comparer)
        {
            if (capacity < 0)
                ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.capacity);
            if (capacity > 0)
                this.Initialize(capacity);
            this.comparer = comparer ?? (IEqualityComparer<TKey>)EqualityComparer<TKey>.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2"/> and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2"/> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="dictionary"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="dictionary"/> contains one or more duplicate keys.</exception>
        //[__DynamicallyInvokable]
        public DictionaryNET(IDictionary<TKey, TValue> dictionary)
          : this(dictionary, (IEqualityComparer<TKey>)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2"/> and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"/>.
        /// </summary>
        /// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2"/> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2"/>.</param><param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"/> for the type of the key.</param><exception cref="T:System.ArgumentNullException"><paramref name="dictionary"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="dictionary"/> contains one or more duplicate keys.</exception>
        //[__DynamicallyInvokable]
        public DictionaryNET(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
          : this(dictionary != null ? dictionary.Count : 0, comparer)
        {
            if (dictionary == null)
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.dictionary);
            foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>)dictionary)
                this.Add(keyValuePair.Key, keyValuePair.Value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class with serialized data.
        /// </summary>
        /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"/>.</param><param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext"/> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2"/>.</param>
        protected DictionaryNET(SerializationInfo info, StreamingContext context)
        {
            HashHelpersNET.SerializationInfoTable.Add((object)this, info);
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param><param name="value">The value of the element to add. The value can be null for reference types.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception><exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.</exception>
        //[__DynamicallyInvokable]
        public void Add(TKey key, TValue value)
        {
            this.Insert(key, value, true);
        }

        //[__DynamicallyInvokable]
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            this.Add(keyValuePair.Key, keyValuePair.Value);
        }

        //[__DynamicallyInvokable]
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int entry = this.FindEntry(keyValuePair.Key);
            return entry >= 0 && EqualityComparer<TValue>.Default.Equals(this.entries[entry].value, keyValuePair.Value);
        }

        //[__DynamicallyInvokable]
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int entry = this.FindEntry(keyValuePair.Key);
            if (entry < 0 || !EqualityComparer<TValue>.Default.Equals(this.entries[entry].value, keyValuePair.Value))
                return false;
            this.Remove(keyValuePair.Key);
            return true;
        }

        /// <summary>
        /// Removes all keys and values from the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </summary>
        //[__DynamicallyInvokable]
        public void Clear()
        {
            if (this.count <= 0)
                return;
            for (int index = 0; index < this.buckets.Length; ++index)
                this.buckets[index] = -1;
            Array.Clear((Array)this.entries, 0, this.count);
            this.freeList = -1;
            this.count = 0;
            this.freeCount = 0;
            this.version = this.version + 1;
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains the specified key.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        //[__DynamicallyInvokable]
        public bool ContainsKey(TKey key)
        {
            return this.FindEntry(key) >= 0;
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains a specific value.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains an element with the specified value; otherwise, false.
        /// </returns>
        /// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.Dictionary`2"/>. The value can be null for reference types.</param>
        //[__DynamicallyInvokable]
        public bool ContainsValue(TValue value)
        {
            if ((object)value == null)
            {
                for (int index = 0; index < this.count; ++index)
                {
                    if (this.entries[index].hashCode >= 0 && (object)this.entries[index].value == null)
                        return true;
                }
            }
            else
            {
                EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
                for (int index = 0; index < this.count; ++index)
                {
                    if (this.entries[index].hashCode >= 0 && @default.Equals(this.entries[index].value, value))
                        return true;
                }
            }
            return false;
        }

        private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.array);
            if (index < 0 || index > array.Length)
                ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.index, ExceptionResourceNET.ArgumentOutOfRange_NeedNonNegNum);
            if (array.Length - index < this.Count)
                ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_ArrayPlusOffTooSmall);
            int num = this.count;
            DictionaryNET<TKey, TValue>.Entry[] entryArray = this.entries;
            for (int index1 = 0; index1 < num; ++index1)
            {
                if (entryArray[index1].hashCode >= 0)
                    array[index++] = new KeyValuePair<TKey, TValue>(entryArray[index1].key, entryArray[index1].value);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator"/> structure for the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </returns>
        //[__DynamicallyInvokable]
        public DictionaryNET<TKey, TValue>.Enumerator GetEnumerator()
        {
            return new DictionaryNET<TKey, TValue>.Enumerator(this, 2);
        }

        //[__DynamicallyInvokable]
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<TKey, TValue>>)new DictionaryNET<TKey, TValue>.Enumerator(this, 2);
        }

        /// <summary>
        /// Implements the <see cref="T:System.Runtime.Serialization.ISerializable"/> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"/> instance.
        /// </summary>
        /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"/> instance.</param><param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext"/> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2"/> instance.</param><exception cref="T:System.ArgumentNullException"><paramref name="info"/> is null.</exception>
        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.info);
            info.AddValue("Version", this.version);
            info.AddValue("Comparer", HashHelpersNET.GetEqualityComparerForSerialization((object)this.comparer), typeof(IEqualityComparer<TKey>));
            info.AddValue("HashSize", this.buckets == null ? 0 : this.buckets.Length);
            if (this.buckets == null)
                return;
            KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.Count];
            this.CopyTo(array, 0);
            info.AddValue("KeyValuePairs", (object)array, typeof(KeyValuePair<TKey, TValue>[]));
        }

        private int FindEntry(TKey key)
        {
            if ((object)key == null)
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            if (this.buckets != null)
            {
                int hashCode = this.comparer.GetHashCode(key) & int.MaxValue;
                for (int index = this.buckets[hashCode % this.buckets.Length]; index >= 0; index = this.entries[index].next)
                {
                    if (this.entries[index].hashCode == hashCode && this.comparer.Equals(this.entries[index].key, key))
                        return index;
                }
            }
            return -1;
        }

        private void Initialize(int capacity)
        {
            int prime = HashHelpersNET.GetPrime(capacity);
            this.buckets = new int[prime];
            for (int index = 0; index < this.buckets.Length; ++index)
                this.buckets[index] = -1;
            this.entries = new DictionaryNET<TKey, TValue>.Entry[prime];
            this.freeList = -1;
        }

        private void Insert(TKey key, TValue value, bool add)
        {
            if ((object)key == null)
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            if (this.buckets == null)
                this.Initialize(0);
            int hashCode = this.comparer.GetHashCode(key) & int.MaxValue;
            int bucketIndex = hashCode % this.buckets.Length;
            int num2 = 0;
            for (int currentIndex = this.buckets[bucketIndex]; currentIndex >= 0; currentIndex = this.entries[currentIndex].next)
            {
                if (this.entries[currentIndex].hashCode == hashCode && this.comparer.Equals(this.entries[currentIndex].key, key))
                {
                    if (add)
                        ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_AddingDuplicate);
                    this.entries[currentIndex].value = value;
                    this.version = this.version + 1;
                    return;
                }
                ++num2;
            }
            int entriesIndex;
            if (this.freeCount > 0)
            {
                entriesIndex = this.freeList;
                this.freeList = this.entries[entriesIndex].next;
                this.freeCount = this.freeCount - 1;
            }
            else
            {
                if (this.count == this.entries.Length)
                {
                    this.Resize();
                    bucketIndex = hashCode % this.buckets.Length;
                }
                entriesIndex = this.count;
                this.count = this.count + 1;
            }
            this.entries[entriesIndex].hashCode = hashCode;
            this.entries[entriesIndex].next = this.buckets[bucketIndex];
            this.entries[entriesIndex].key = key;
            this.entries[entriesIndex].value = value;
            this.buckets[bucketIndex] = entriesIndex;
            this.version = this.version + 1;
            if (num2 <= 100 || !HashHelpersNET.IsWellKnownEqualityComparer((object)this.comparer))
                return;
            this.comparer = (IEqualityComparer<TKey>)HashHelpersNET.GetRandomizedEqualityComparer((object)this.comparer);
            this.Resize(this.entries.Length, true);
        }

        /// <summary>
        /// Implements the <see cref="T:System.Runtime.Serialization.ISerializable"/> interface and raises the deserialization event when the deserialization is complete.
        /// </summary>
        /// <param name="sender">The source of the deserialization event.</param><exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object associated with the current <see cref="T:System.Collections.Generic.Dictionary`2"/> instance is invalid.</exception>
        public virtual void OnDeserialization(object sender)
        {
            SerializationInfo serializationInfo;
            HashHelpersNET.SerializationInfoTable.TryGetValue((object)this, out serializationInfo);
            if (serializationInfo == null)
                return;
            int int32_1 = serializationInfo.GetInt32("Version");
            int int32_2 = serializationInfo.GetInt32("HashSize");
            this.comparer = (IEqualityComparer<TKey>)serializationInfo.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
            if (int32_2 != 0)
            {
                this.buckets = new int[int32_2];
                for (int index = 0; index < this.buckets.Length; ++index)
                    this.buckets[index] = -1;
                this.entries = new DictionaryNET<TKey, TValue>.Entry[int32_2];
                this.freeList = -1;
                KeyValuePair<TKey, TValue>[] keyValuePairArray = (KeyValuePair<TKey, TValue>[])serializationInfo.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
                if (keyValuePairArray == null)
                    ThrowHelperNET.ThrowSerializationException(ExceptionResourceNET.Serialization_MissingKeys);
                for (int index = 0; index < keyValuePairArray.Length; ++index)
                {
                    if ((object)keyValuePairArray[index].Key == null)
                        ThrowHelperNET.ThrowSerializationException(ExceptionResourceNET.Serialization_NullKey);
                    this.Insert(keyValuePairArray[index].Key, keyValuePairArray[index].Value, true);
                }
            }
            else
                this.buckets = (int[])null;
            this.version = int32_1;
            HashHelpersNET.SerializationInfoTable.Remove((object)this);
        }

        private void Resize()
        {
            this.Resize(HashHelpersNET.ExpandPrime(this.count), false);
        }

        private void Resize(int newSize, bool forceNewHashCodes)
        {
            int[] numArray = new int[newSize];
            for (int index = 0; index < numArray.Length; ++index)
                numArray[index] = -1;
            DictionaryNET<TKey, TValue>.Entry[] entryArray = new DictionaryNET<TKey, TValue>.Entry[newSize];
            Array.Copy((Array)this.entries, 0, (Array)entryArray, 0, this.count);
            if (forceNewHashCodes)
            {
                for (int index = 0; index < this.count; ++index)
                {
                    if (entryArray[index].hashCode != -1)
                        entryArray[index].hashCode = this.comparer.GetHashCode(entryArray[index].key) & int.MaxValue;
                }
            }
            for (int index1 = 0; index1 < this.count; ++index1)
            {
                if (entryArray[index1].hashCode >= 0)
                {
                    int index2 = entryArray[index1].hashCode % newSize;
                    entryArray[index1].next = numArray[index2];
                    numArray[index2] = index1;
                }
            }
            this.buckets = numArray;
            this.entries = entryArray;
        }

        /// <summary>
        /// Removes the value with the specified key from the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </summary>
        /// 
        /// <returns>
        /// true if the element is successfully found and removed; otherwise, false.  This method returns false if <paramref name="key"/> is not found in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </returns>
        /// <param name="key">The key of the element to remove.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        //[__DynamicallyInvokable]
        public bool Remove(TKey key)
        {
            if ((object)key == null)
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            if (this.buckets != null)
            {
                int hashCode = this.comparer.GetHashCode(key) & int.MaxValue;
                int bucketIndex = hashCode % this.buckets.Length;
                int previousIndex = -1;
                for (int currentIndex = this.buckets[bucketIndex]; currentIndex >= 0; currentIndex = this.entries[currentIndex].next)
                {
                    if (this.entries[currentIndex].hashCode == hashCode && this.comparer.Equals(this.entries[currentIndex].key, key))
                    {
                        if (previousIndex < 0)
                            this.buckets[bucketIndex] = this.entries[currentIndex].next;
                        else
                            this.entries[previousIndex].next = this.entries[currentIndex].next;
                        this.entries[currentIndex].hashCode = -1;
                        this.entries[currentIndex].next = this.freeList;
                        this.entries[currentIndex].key = default(TKey);
                        this.entries[currentIndex].value = default(TValue);
                        this.freeList = currentIndex;
                        this.freeCount = this.freeCount + 1;
                        this.version = this.version + 1;
                        return true;
                    }
                    previousIndex = currentIndex;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">The key of the value to get.</param><param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        //[__DynamicallyInvokable]
        public bool TryGetValue(TKey key, out TValue value)
        {
            int entry = this.FindEntry(key);
            if (entry >= 0)
            {
                value = this.entries[entry].value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        internal TValue GetValueOrDefault(TKey key)
        {
            int entry = this.FindEntry(key);
            if (entry >= 0)
                return this.entries[entry].value;
            return default(TValue);
        }

        //[__DynamicallyInvokable]
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            this.CopyTo(array, index);
        }

        //[__DynamicallyInvokable]
        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.array);
            if (array.Rank != 1)
                ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_RankMultiDimNotSupported);
            if (array.GetLowerBound(0) != 0)
                ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_NonZeroLowerBound);
            if (index < 0 || index > array.Length)
                ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.index, ExceptionResourceNET.ArgumentOutOfRange_NeedNonNegNum);
            if (array.Length - index < this.Count)
                ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_ArrayPlusOffTooSmall);
            KeyValuePair<TKey, TValue>[] array1 = array as KeyValuePair<TKey, TValue>[];
            if (array1 != null)
                this.CopyTo(array1, index);
            else if (array is DictionaryEntry[])
            {
                DictionaryEntry[] dictionaryEntryArray = array as DictionaryEntry[];
                DictionaryNET<TKey, TValue>.Entry[] entryArray = this.entries;
                for (int index1 = 0; index1 < this.count; ++index1)
                {
                    if (entryArray[index1].hashCode >= 0)
                        dictionaryEntryArray[index++] = new DictionaryEntry((object)entryArray[index1].key, (object)entryArray[index1].value);
                }
            }
            else
            {
                object[] objArray = array as object[];
                if (objArray == null)
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_InvalidArrayType);
                try
                {
                    int num = this.count;
                    DictionaryNET<TKey, TValue>.Entry[] entryArray = this.entries;
                    for (int index1 = 0; index1 < num; ++index1)
                    {
                        if (entryArray[index1].hashCode >= 0)
                            objArray[index++] = (object)new KeyValuePair<TKey, TValue>(entryArray[index1].key, entryArray[index1].value);
                    }
                }
                catch (ArrayTypeMismatchException ex)
                {
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_InvalidArrayType);
                }
            }
        }

        //[__DynamicallyInvokable]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new DictionaryNET<TKey, TValue>.Enumerator(this, 2);
        }

        private static bool IsCompatibleKey(object key)
        {
            if (key == null)
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            return key is TKey;
        }

        //[__DynamicallyInvokable]
        void IDictionary.Add(object key, object value)
        {
            if (key == null)
                ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.key);
            ThrowHelperNET.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgumentNET.value);
            try
            {
                TKey key1 = (TKey)key;
                try
                {
                    this.Add(key1, (TValue)value);
                }
                catch (InvalidCastException ex)
                {
                    ThrowHelperNET.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                }
            }
            catch (InvalidCastException ex)
            {
                ThrowHelperNET.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
            }
        }

        //[__DynamicallyInvokable]
        bool IDictionary.Contains(object key)
        {
            if (DictionaryNET<TKey, TValue>.IsCompatibleKey(key))
                return this.ContainsKey((TKey)key);
            return false;
        }

        //[__DynamicallyInvokable]
        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return (IDictionaryEnumerator)new DictionaryNET<TKey, TValue>.Enumerator(this, 1);
        }

        //[__DynamicallyInvokable]
        void IDictionary.Remove(object key)
        {
            if (!DictionaryNET<TKey, TValue>.IsCompatibleKey(key))
                return;
            this.Remove((TKey)key);
        }

        public struct Entry
        {
            public int hashCode;
            public int next;
            public TKey key;
            public TValue value;
        }

        /// <summary>
        /// Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </summary>
        //[__DynamicallyInvokable]
        [Serializable]
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
        {
            private DictionaryNET<TKey, TValue> dictionary;
            private int version;
            private int index;
            private KeyValuePair<TKey, TValue> current;
            private int getEnumeratorRetType;
            internal const int DictEntry = 1;
            internal const int KeyValuePair = 2;

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            /// 
            /// <returns>
            /// The element in the <see cref="T:System.Collections.Generic.Dictionary`2"/> at the current position of the enumerator.
            /// </returns>
            //[__DynamicallyInvokable]
            public KeyValuePair<TKey, TValue> Current
            {
                //[__DynamicallyInvokable]
                get
                {
                    return this.current;
                }
            }

            //[__DynamicallyInvokable]
            object IEnumerator.Current
            {
                //[__DynamicallyInvokable]
                get
                {
                    if (this.index == 0 || this.index == this.dictionary.count + 1)
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                    if (this.getEnumeratorRetType == 1)
                        return (object)new DictionaryEntry((object)this.current.Key, (object)this.current.Value);
                    return (object)new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
                }
            }

            //[__DynamicallyInvokable]
            DictionaryEntry IDictionaryEnumerator.Entry
            {
                //[__DynamicallyInvokable]
                get
                {
                    if (this.index == 0 || this.index == this.dictionary.count + 1)
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                    return new DictionaryEntry((object)this.current.Key, (object)this.current.Value);
                }
            }

            //[__DynamicallyInvokable]
            object IDictionaryEnumerator.Key
            {
                //[__DynamicallyInvokable]
                get
                {
                    if (this.index == 0 || this.index == this.dictionary.count + 1)
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                    return (object)this.current.Key;
                }
            }

            //[__DynamicallyInvokable]
            object IDictionaryEnumerator.Value
            {
                //[__DynamicallyInvokable]
                get
                {
                    if (this.index == 0 || this.index == this.dictionary.count + 1)
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                    return (object)this.current.Value;
                }
            }

            internal Enumerator(DictionaryNET<TKey, TValue> dictionary, int getEnumeratorRetType)
            {
                this.dictionary = dictionary;
                this.version = dictionary.version;
                this.index = 0;
                this.getEnumeratorRetType = getEnumeratorRetType;
                this.current = new KeyValuePair<TKey, TValue>();
            }

            /// <summary>
            /// Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
            /// </summary>
            /// 
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
            //[__DynamicallyInvokable]
            public bool MoveNext()
            {
                if (this.version != this.dictionary.version)
                    ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumFailedVersion);
                for (; (uint)this.index < (uint)this.dictionary.count; this.index = this.index + 1)
                {
                    if (this.dictionary.entries[this.index].hashCode >= 0)
                    {
                        this.current = new KeyValuePair<TKey, TValue>(this.dictionary.entries[this.index].key, this.dictionary.entries[this.index].value);
                        this.index = this.index + 1;
                        return true;
                    }
                }
                this.index = this.dictionary.count + 1;
                this.current = new KeyValuePair<TKey, TValue>();
                return false;
            }

            /// <summary>
            /// Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator"/>.
            /// </summary>
            //[__DynamicallyInvokable]
            public void Dispose()
            {
            }

            //[__DynamicallyInvokable]
            void IEnumerator.Reset()
            {
                if (this.version != this.dictionary.version)
                    ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumFailedVersion);
                this.index = 0;
                this.current = new KeyValuePair<TKey, TValue>();
            }
        }

        /// <summary>
        /// Represents the collection of keys in a <see cref="T:System.Collections.Generic.Dictionary`2"/>. This class cannot be inherited.
        /// </summary>
        //[DebuggerTypeProxy(typeof(Mscorlib_DictionaryKeyCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
        //[__DynamicallyInvokable]
        [Serializable]
        public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection
#if CS5
            , IReadOnlyCollection<TKey>
#endif
        {
            private DictionaryNET<TKey, TValue> dictionary;

            /// <summary>
            /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
            /// </summary>
            /// 
            /// <returns>
            /// The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.Retrieving the value of this property is an O(1) operation.
            /// </returns>
            //[__DynamicallyInvokable]
            public int Count
            {
                //[__DynamicallyInvokable]
                get
                {
                    return this.dictionary.Count;
                }
            }

            //[__DynamicallyInvokable]
            bool ICollection<TKey>.IsReadOnly
            {
                //[__DynamicallyInvokable]
                get
                {
                    return true;
                }
            }

            //[__DynamicallyInvokable]
            bool ICollection.IsSynchronized
            {
                //[__DynamicallyInvokable]
                get
                {
                    return false;
                }
            }

            //[__DynamicallyInvokable]
            object ICollection.SyncRoot
            {
                //[__DynamicallyInvokable]
                get
                {
                    return ((ICollection)this.dictionary).SyncRoot;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/> class that reflects the keys in the specified <see cref="T:System.Collections.Generic.Dictionary`2"/>.
            /// </summary>
            /// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2"/> whose keys are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="dictionary"/> is null.</exception>
            //[__DynamicallyInvokable]
            public KeyCollection(DictionaryNET<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.dictionary);
                this.dictionary = dictionary;
            }

            /// <summary>
            /// Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
            /// </summary>
            /// 
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator"/> for the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
            /// </returns>
            //[__DynamicallyInvokable]
            public DictionaryNET<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
            {
                return new DictionaryNET<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
            }

            /// <summary>
            /// Copies the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/> elements to an existing one-dimensional <see cref="T:System.Array"/>, starting at the specified array index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception><exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.</exception>
            //[__DynamicallyInvokable]
            public void CopyTo(TKey[] array, int index)
            {
                if (array == null)
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.array);
                if (index < 0 || index > array.Length)
                    ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.index, ExceptionResourceNET.ArgumentOutOfRange_NeedNonNegNum);
                if (array.Length - index < this.dictionary.Count)
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_ArrayPlusOffTooSmall);
                int num = this.dictionary.count;
                DictionaryNET<TKey, TValue>.Entry[] entryArray = this.dictionary.entries;
                for (int index1 = 0; index1 < num; ++index1)
                {
                    if (entryArray[index1].hashCode >= 0)
                        array[index++] = entryArray[index1].key;
                }
            }

            //[__DynamicallyInvokable]
            void ICollection<TKey>.Add(TKey item)
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_KeyCollectionSet);
            }

            //[__DynamicallyInvokable]
            void ICollection<TKey>.Clear()
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_KeyCollectionSet);
            }

            //[__DynamicallyInvokable]
            bool ICollection<TKey>.Contains(TKey item)
            {
                return this.dictionary.ContainsKey(item);
            }

            //[__DynamicallyInvokable]
            bool ICollection<TKey>.Remove(TKey item)
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_KeyCollectionSet);
                return false;
            }

            //[__DynamicallyInvokable]
            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
            {
                return (IEnumerator<TKey>)new DictionaryNET<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
            }

            //[__DynamicallyInvokable]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)new DictionaryNET<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
            }

            //[__DynamicallyInvokable]
            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.array);
                if (array.Rank != 1)
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_RankMultiDimNotSupported);
                if (array.GetLowerBound(0) != 0)
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_NonZeroLowerBound);
                if (index < 0 || index > array.Length)
                    ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.index, ExceptionResourceNET.ArgumentOutOfRange_NeedNonNegNum);
                if (array.Length - index < this.dictionary.Count)
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_ArrayPlusOffTooSmall);
                TKey[] array1 = array as TKey[];
                if (array1 != null)
                {
                    this.CopyTo(array1, index);
                }
                else
                {
                    object[] objArray = array as object[];
                    if (objArray == null)
                        ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_InvalidArrayType);
                    int num = this.dictionary.count;
                    DictionaryNET<TKey, TValue>.Entry[] entryArray = this.dictionary.entries;
                    try
                    {
                        for (int index1 = 0; index1 < num; ++index1)
                        {
                            if (entryArray[index1].hashCode >= 0)
                                objArray[index++] = (object)entryArray[index1].key;
                        }
                    }
                    catch (ArrayTypeMismatchException ex)
                    {
                        ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_InvalidArrayType);
                    }
                }
            }

            /// <summary>
            /// Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
            /// </summary>
            //[__DynamicallyInvokable]
            [Serializable]
            public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
            {
                private DictionaryNET<TKey, TValue> dictionary;
                private int index;
                private int version;
                private TKey currentKey;

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                /// 
                /// <returns>
                /// The element in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/> at the current position of the enumerator.
                /// </returns>
                //[__DynamicallyInvokable]
                public TKey Current
                {
                    //[__DynamicallyInvokable]
                    get
                    {
                        return this.currentKey;
                    }
                }

                //[__DynamicallyInvokable]
                object IEnumerator.Current
                {
                    //[__DynamicallyInvokable]
                    get
                    {
                        if (this.index == 0 || this.index == this.dictionary.count + 1)
                            ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                        return (object)this.currentKey;
                    }
                }

                internal Enumerator(DictionaryNET<TKey, TValue> dictionary)
                {
                    this.dictionary = dictionary;
                    this.version = dictionary.version;
                    this.index = 0;
                    this.currentKey = default(TKey);
                }

                /// <summary>
                /// Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator"/>.
                /// </summary>
                //[__DynamicallyInvokable]
                public void Dispose()
                {
                }

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
                /// </summary>
                /// 
                /// <returns>
                /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
                /// </returns>
                /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
                //[__DynamicallyInvokable]
                public bool MoveNext()
                {
                    if (this.version != this.dictionary.version)
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumFailedVersion);
                    for (; (uint)this.index < (uint)this.dictionary.count; this.index = this.index + 1)
                    {
                        if (this.dictionary.entries[this.index].hashCode >= 0)
                        {
                            this.currentKey = this.dictionary.entries[this.index].key;
                            this.index = this.index + 1;
                            return true;
                        }
                    }
                    this.index = this.dictionary.count + 1;
                    this.currentKey = default(TKey);
                    return false;
                }

                //[__DynamicallyInvokable]
                void IEnumerator.Reset()
                {
                    if (this.version != this.dictionary.version)
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumFailedVersion);
                    this.index = 0;
                    this.currentKey = default(TKey);
                }
            }
        }

        /// <summary>
        /// Represents the collection of values in a <see cref="T:System.Collections.Generic.Dictionary`2"/>. This class cannot be inherited.
        /// </summary>
        //[DebuggerTypeProxy(typeof(Mscorlib_DictionaryValueCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
        //[__DynamicallyInvokable]
        [Serializable]
        public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection
#if CS5
            , IReadOnlyCollection<TValue>
#endif
        {
            private DictionaryNET<TKey, TValue> dictionary;

            /// <summary>
            /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            /// </summary>
            /// 
            /// <returns>
            /// The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            /// </returns>
            //[__DynamicallyInvokable]
            public int Count
            {
                //[__DynamicallyInvokable]
                get
                {
                    return this.dictionary.Count;
                }
            }

            //[__DynamicallyInvokable]
            bool ICollection<TValue>.IsReadOnly
            {
                //[__DynamicallyInvokable]
                get
                {
                    return true;
                }
            }

            //[__DynamicallyInvokable]
            bool ICollection.IsSynchronized
            {
                //[__DynamicallyInvokable]
                get
                {
                    return false;
                }
            }

            //[__DynamicallyInvokable]
            object ICollection.SyncRoot
            {
                //[__DynamicallyInvokable]
                get
                {
                    return ((ICollection)this.dictionary).SyncRoot;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/> class that reflects the values in the specified <see cref="T:System.Collections.Generic.Dictionary`2"/>.
            /// </summary>
            /// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2"/> whose values are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="dictionary"/> is null.</exception>
            //[__DynamicallyInvokable]
            public ValueCollection(DictionaryNET<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.dictionary);
                this.dictionary = dictionary;
            }

            /// <summary>
            /// Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            /// </summary>
            /// 
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator"/> for the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            /// </returns>
            //[__DynamicallyInvokable]
            public DictionaryNET<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
            {
                return new DictionaryNET<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
            }

            /// <summary>
            /// Copies the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/> elements to an existing one-dimensional <see cref="T:System.Array"/>, starting at the specified array index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception><exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.</exception>
            //[__DynamicallyInvokable]
            public void CopyTo(TValue[] array, int index)
            {
                if (array == null)
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.array);
                if (index < 0 || index > array.Length)
                    ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.index, ExceptionResourceNET.ArgumentOutOfRange_NeedNonNegNum);
                if (array.Length - index < this.dictionary.Count)
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_ArrayPlusOffTooSmall);
                int num = this.dictionary.count;
                DictionaryNET<TKey, TValue>.Entry[] entryArray = this.dictionary.entries;
                for (int index1 = 0; index1 < num; ++index1)
                {
                    if (entryArray[index1].hashCode >= 0)
                        array[index++] = entryArray[index1].value;
                }
            }

            //[__DynamicallyInvokable]
            void ICollection<TValue>.Add(TValue item)
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_ValueCollectionSet);
            }

            //[__DynamicallyInvokable]
            bool ICollection<TValue>.Remove(TValue item)
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_ValueCollectionSet);
                return false;
            }

            //[__DynamicallyInvokable]
            void ICollection<TValue>.Clear()
            {
                ThrowHelperNET.ThrowNotSupportedException(ExceptionResourceNET.NotSupported_ValueCollectionSet);
            }

            //[__DynamicallyInvokable]
            bool ICollection<TValue>.Contains(TValue item)
            {
                return this.dictionary.ContainsValue(item);
            }

            //[__DynamicallyInvokable]
            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            {
                return (IEnumerator<TValue>)new DictionaryNET<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
            }

            //[__DynamicallyInvokable]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)new DictionaryNET<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
            }

            //[__DynamicallyInvokable]
            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                    ThrowHelperNET.ThrowArgumentNullException(ExceptionArgumentNET.array);
                if (array.Rank != 1)
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_RankMultiDimNotSupported);
                if (array.GetLowerBound(0) != 0)
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_NonZeroLowerBound);
                if (index < 0 || index > array.Length)
                    ThrowHelperNET.ThrowArgumentOutOfRangeException(ExceptionArgumentNET.index, ExceptionResourceNET.ArgumentOutOfRange_NeedNonNegNum);
                if (array.Length - index < this.dictionary.Count)
                    ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Arg_ArrayPlusOffTooSmall);
                TValue[] array1 = array as TValue[];
                if (array1 != null)
                {
                    this.CopyTo(array1, index);
                }
                else
                {
                    object[] objArray = array as object[];
                    if (objArray == null)
                        ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_InvalidArrayType);
                    int num = this.dictionary.count;
                    DictionaryNET<TKey, TValue>.Entry[] entryArray = this.dictionary.entries;
                    try
                    {
                        for (int index1 = 0; index1 < num; ++index1)
                        {
                            if (entryArray[index1].hashCode >= 0)
                                objArray[index++] = (object)entryArray[index1].value;
                        }
                    }
                    catch (ArrayTypeMismatchException ex)
                    {
                        ThrowHelperNET.ThrowArgumentException(ExceptionResourceNET.Argument_InvalidArrayType);
                    }
                }
            }

            /// <summary>
            /// Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            /// </summary>
            //[__DynamicallyInvokable]
            [Serializable]
            public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
            {
                private DictionaryNET<TKey, TValue> dictionary;
                private int index;
                private int version;
                private TValue currentValue;

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                /// 
                /// <returns>
                /// The element in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/> at the current position of the enumerator.
                /// </returns>
                //[__DynamicallyInvokable]
                public TValue Current
                {
                    //[__DynamicallyInvokable]
                    get
                    {
                        return this.currentValue;
                    }
                }

                //[__DynamicallyInvokable]
                object IEnumerator.Current
                {
                    //[__DynamicallyInvokable]
                    get
                    {
                        if (this.index == 0 || this.index == this.dictionary.count + 1)
                            ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumOpCantHappen);
                        return (object)this.currentValue;
                    }
                }

                internal Enumerator(DictionaryNET<TKey, TValue> dictionary)
                {
                    this.dictionary = dictionary;
                    this.version = dictionary.version;
                    this.index = 0;
                    this.currentValue = default(TValue);
                }

                /// <summary>
                /// Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator"/>.
                /// </summary>
                //[__DynamicallyInvokable]
                public void Dispose()
                {
                }

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
                /// </summary>
                /// 
                /// <returns>
                /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
                /// </returns>
                /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
                //[__DynamicallyInvokable]
                public bool MoveNext()
                {
                    if (this.version != this.dictionary.version)
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumFailedVersion);
                    for (; (uint)this.index < (uint)this.dictionary.count; this.index = this.index + 1)
                    {
                        if (this.dictionary.entries[this.index].hashCode >= 0)
                        {
                            this.currentValue = this.dictionary.entries[this.index].value;
                            this.index = this.index + 1;
                            return true;
                        }
                    }
                    this.index = this.dictionary.count + 1;
                    this.currentValue = default(TValue);
                    return false;
                }

                //[__DynamicallyInvokable]
                void IEnumerator.Reset()
                {
                    if (this.version != this.dictionary.version)
                        ThrowHelperNET.ThrowInvalidOperationException(ExceptionResourceNET.InvalidOperation_EnumFailedVersion);
                    this.index = 0;
                    this.currentValue = default(TValue);
                }
            }
        }
    }
}

