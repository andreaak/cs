using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresAndAlgorithms.HashTable
{
    // Создание массива списков с уникалыными индексами и записями узлов (пары-ключ значение )

    class HashTableArray<TKey, TValue>
    {
        HashTableNode<TKey, TValue>[] _array;  // создание массива узлов (пары ключ-значение)  

        public HashTableArray(int capacity)
        {
            _array = new HashTableNode<TKey, TValue>[capacity]; // выделение памяти под массив 
        }

        #region Получения индекса для записи списка узлов (ключ-значение) в массив

        private int GetIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode() % Capacity);
        }

        #endregion

        #region Capacity

        public int Capacity
        {
            get
            {
                return _array.Length;
            }
        }

        #endregion

        /// <summary>
        /// Removes every item from the hash table array
        /// </summary>
        public void Clear()
        {
            foreach (HashTableNode<TKey, TValue> node in _array)
            {
                node.Clear();
            }
        }

        #region Добавление элемента

        public void Add(TKey key, TValue value)
        {

            int index = GetIndex(key);

            HashTableNode<TKey, TValue> nodes = _array[index];

            if (nodes == null)
            {
                nodes = new HashTableNode<TKey, TValue>();
                _array[index] = nodes;
            }

            nodes.Add(key, value);

        }

        #endregion

        #region Нумераторы

        public System.Collections.Generic.IEnumerator<HashTablePair<TKey, TValue>> GetEnumerator()
        {

            foreach (HashTableNode<TKey, TValue> node in _array.Where(node => node != null))
            {
                foreach (HashTablePair<TKey, TValue> pair in node.Items)
                {
                    yield return pair;
                }
            }

        }

        public IEnumerable<HashTablePair<TKey, TValue>> Items
        {
            get
            {


                foreach (HashTableNode<TKey, TValue> node in _array.Where(node => node != null))
                {
                    foreach (HashTablePair<TKey, TValue> pair in node.Items)
                    {
                        yield return pair;
                    }
                }

            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (HashTableNode<TKey, TValue> node in _array.Where(node => node != null))
                {
                    foreach (TValue value in node.Values)
                    {
                        yield return value;
                    }
                }
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                foreach (HashTableNode<TKey, TValue> node in _array.Where(node => node != null))
                {
                    foreach (TKey key in node.Keys)
                    {
                        yield return key;
                    }
                }
            }
        }

        #endregion

        #region Изменение значения по ключу

        public void Update(TKey key, TValue value)
        {
            HashTableNode<TKey, TValue> nodes = _array[GetIndex(key)];

            if (nodes == null)
            {
                throw new ArgumentException("Такого ключа нет в хеш-таблице", "key");
            }

            nodes.Update(key, value);
        }

        #endregion

        #region Считывание элемента по индексу

        public bool TryGetValue(TKey key, out TValue value)
        {
            HashTableNode<TKey, TValue> nodes = _array[GetIndex(key)];

            if (nodes != null)
            {
                return nodes.TryGetValue(key, out value);
            }

            value = default(TValue);
            return false;
        }

        #endregion

        #region Удаление элемента по ключу

        public bool Remove(TKey key)
        {
            HashTableNode<TKey, TValue> nodes = _array[GetIndex(key)];

            if (nodes != null)
            {
                return nodes.Remove(key);
            }

            return false;
        }
        
        #endregion



    }

        

}
