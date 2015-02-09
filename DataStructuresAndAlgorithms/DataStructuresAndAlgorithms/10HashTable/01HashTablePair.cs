using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithms.HashTable
{
    public class HashTablePair<TKey, TValue>
    {
        // Конструктор для класса ключ-значене, которые хранятся в хеш-таблице 

        public HashTablePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        // Ключ нельзя поменять, так как это повлияет на индексацию таблицы. 

        public TKey Key
        {
            get;
            private set;
        }

        // Значение

        public TValue Value
        {
            get;
            set;
        }
    }
}
