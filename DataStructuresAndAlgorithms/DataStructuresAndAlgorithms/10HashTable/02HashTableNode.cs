using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithms.HashTable
{
    public class HashTableNode<TKey, TValue>
    {

        LinkedList<HashTablePair<TKey, TValue>> _items;

        #region Добавление нового узла (пары ключ-значение)
        
        public void Add(TKey key, TValue value)
        {

            // Создание таблицы на осноые двусвязного списка     
            if (_items == null)
            {
                _items = new LinkedList<HashTablePair<TKey, TValue>>();
            }
            else
            {
                // Несколько одинаковых элементов могут находится в одной таблице, 
                // но ключи по которым они располагаются должны быть разными. 

                foreach (HashTablePair<TKey, TValue> pair in _items)
                {
                    // Если такой ключ уже существует - выдать исключение

                    if (pair.Key.Equals(key))
                    {
                        throw new ArgumentException("Такой ключ уже существует");
                    }
                }
            }

            // Добавление нового узла.     
            _items.AddLast(new HashTablePair<TKey, TValue>(key, value));
        }

        #endregion    

        #region Нумераторы

        public System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
        {

            if (_items != null)
            {
                foreach (HashTablePair<TKey, TValue> node in _items)
                {
                    yield return node.Value;
                }
            }

        }

        public System.Collections.Generic.IEnumerator<TKey> GetKeyEnumerator()
        {
            if (_items != null)
            {
                foreach (HashTablePair<TKey, TValue> node in _items)
                {
                    yield return node.Key;
                }
            }

        }

        public IEnumerable<HashTablePair<TKey, TValue>> Items
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTablePair<TKey, TValue> node in _items)
                    {
                        yield return node;
                    }
                }
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTablePair<TKey, TValue> node in _items)
                    {
                        yield return node.Value;
                    }
                }
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTablePair<TKey, TValue> node in _items)
                    {
                        yield return node.Key;
                    }
                }
            }
        }

        #endregion    

        #region Перезапись значения по ключу.

        public void Update(TKey key, TValue value)
        {
            bool updated = false;

            if (_items != null)
            {
                // Проверка каждого элемента в списке по ключу               
                foreach (HashTablePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        // Перезапись значения                 
                        pair.Value = value;
                        updated = true;
                        break;
                    }
                }
            }

            if (!updated)
            {
                throw new ArgumentException("Такой ключ не был найден");
            }
        }

        #endregion    
    
        #region Поиск значения по ключу.

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue); // запись значения по умолчанию
            bool found = false;

            if (_items != null)
            {
                foreach (HashTablePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        value = pair.Value;
                        found = true;
                        break;
                    }
                }
            }
            return found;
        }

        #endregion

        #region Удаление узла по ключу

        public bool Remove(TKey key)
        {
            bool removed = false;

            if (_items != null)
            {
                LinkedListNode<HashTablePair<TKey, TValue>> current = _items.First;

                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        _items.Remove(current);
                        removed = true;
                        break;
                    }
                    current = current.Next;
                }
            }
            return removed;
        }
        #endregion

        #region Удаление всех элементов из списка

        public void Clear()
        {
            if (_items != null)
            {
                _items.Clear();
            }
        }


        #endregion

    }


}
