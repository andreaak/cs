using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithms.Set
{
    public class Set<T> : IEnumerable<T> where T : IComparable<T>
    {
        private readonly List<T> _items = new List<T>();

        public Set()
        {
        }
        
        public Set(IEnumerable<T> items)
        {
            AddRange(items);
        } 
        
        #region Добавление элементов в множество

        public void Add(T item)
        {
            // Если такой элемент уже существует в множестве, выдать исключение
            if (Contains(item))
            {
                throw new InvalidOperationException("Такое значение уже содержится в множестве");
            }

            _items.Add(item);
        }

        #endregion

        #region Проверка наличия элемента в множестве
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }
        #endregion

        #region Количество элементов в множестве
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }
        #endregion

        #region Перечислитель

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        #endregion

        #region Добавление нового множества элементов

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        #endregion

        #region Удаление элементов из множества

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        #endregion
        
        #region Объединение множеств

        public Set<T> Union(Set<T> other)
        {
            Set<T> result = new Set<T>(_items);

            foreach (T item in other._items)
            {
                if (!Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        #endregion

        #region Пересечение множеств

        public Set<T> Intersection(Set<T> other)
        {
            Set<T> result = new Set<T>();

            foreach (T item in _items)
            {
                if (other._items.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        #endregion

        #region Разность множеств

        public Set<T> Difference(Set<T> other)
        {
            Set<T> result = new Set<T>(_items);
            foreach (T item in other._items)
            {
                result.Remove(item);
            }

            return result;
        }

        #endregion

        #region Симметрическая разность множеств

        public Set<T> SymmetricDifference(Set<T> other)
        {
            Set<T> union = Union(other);                // 1 2 3 4 5 6
            Set<T> intersection = Intersection(other);  // 3 4
            return union.Difference(intersection);      // 1 2 5 6
        }

        #endregion

        #region Подмножество

        public bool IsSubset(Set<T> other)
        {

            Set<T> result = new Set<T>(_items);

            foreach (T item in other._items)
            {
                result.Remove(item);
            }

            if (result.Count == 0)
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        #endregion
 
    }
}
