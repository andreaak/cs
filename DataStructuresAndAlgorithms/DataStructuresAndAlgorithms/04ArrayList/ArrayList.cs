using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresAndAlgorithms.ArrayList
{
    public class ArrayList<T> : IEnumerable
    {
        T[] _items;

        // Конструктор по умолчанию
        public ArrayList()
            : this(0)
        {

        }

        // Конструктор с параметром 
        public ArrayList(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException("length");
            }
            _items = new T[length];
        }

        // Конструктор с параметром
        public ArrayList(ICollection<T> list)
        {
            int index = 0;
            _items = new T[list.Count];

            foreach (var element in list)
            {
                Count++;
                _items[index++] = element;

            }
        }

        // Подсчет элементов в массиве
        public int Count
        {
            get;
            internal set;

        }

        // Расширение массива
        private void GrowArray()
        {
            int newLength = _items.Length == 0 ? 4 : _items.Length << 1;
            T[] newArray = new T[newLength];
            _items.CopyTo(newArray, 0);
            _items = newArray;
        }

        // Метод возврящает перечислитель, осуществляет перебор коллекции.
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _items[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Метод добавляет новый элемент в конец массива 
        public void Add(T item)
        {
            {
                if (_items.Length == Count)
                {
                    GrowArray();
                }

                _items[Count++] = item;
            }
        }

        public void Insert(int index, T item)
        {
            if (index > Count)
            {
                throw new IndexOutOfRangeException();
            }

            //Расширение массива.
            if (_items.Length == Count)
            {
                GrowArray();
            }

            // Сдвиг всех элементов, после вставки элемента, в право.
            Array.Copy(_items, index, _items, index + 1, Count - index);

            _items[index] = item;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            int shiftStart = index + 1;

            if (shiftStart < Count) //  если удаляется не последний элемент массива     
            {
                // Сдвиг массива на один элемент влево                                       
                Array.Copy(_items, shiftStart, _items, index, Count - shiftStart);
            }
            Count--;
        }

        // Удаление указаного элемента
        public bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public T this[int index]
        {
            get
            {
                if (index < Count)
                {
                    return _items[index];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index < Count)
                {
                    _items[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        // Поиск индекса элемента
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        // Метод опередиляет пренадлежность элемента к массиву
        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        } 
    }
}
