using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithms.Deque
{
    public class DequeArray<T>
    {
        T[] _items = new T[0];

        // Количество элементов в двусвязной очереди     
        int _size = 0;

        // Указатель на первый (самый старый) элемент очереди.     
        int _head = 0;

        // Указатель на последний (новый) элемент очереди.     
        int _tail = -1;

        #region Метод увеличивает внутренний массива

        private void allocateNewArray(int startingIndex)
        {

            // начальный размер массива равен 4, при увеличение массива размер удваивается.

            int newLength = (_size == 0) ? 4 : _size * 2;

            T[] newArray = new T[newLength];

            if (_size > 0)
            {
                int targetIndex = startingIndex;

                // Копирование содержимого ...         
                // Если не было обхода массива индексами, то копирование элементов.         
                // в ином случае копирование с указателя head до конца массива end, а затем от 0 до tail.  
                // Если указатель tail меньше чем head, осуществить им обход массива.         

                if (_tail < _head)
                {
                    // Копирование от _items[head].._items[end] -> newArray[0]..newArray[N].             
                    for (int index = _head; index < _items.Length; index++)
                    {

                        newArray[targetIndex] = _items[index];
                        targetIndex++;
                    }

                    // Копирование от _items[0].._items[tail] -> newArray[N+1]..             

                    for (int index = 0; index <= _tail; index++)
                    {
                        newArray[targetIndex] = _items[index];
                        targetIndex++;
                    }
                }
                else
                {
                    // Копировать от _items[head].._items[tail] -> newArray[0]..newArray[N]             
                    for (int index = _head; index <= _tail; index++)
                    {
                        newArray[targetIndex] = _items[index];
                        targetIndex++;
                    }
                }

                _head = startingIndex;
                _tail = targetIndex - 1;


            }
            else
            {    // Если массив пустой.         
                _head = 0;
                _tail = -1;
            }

            _items = newArray;
        }

        #endregion

        #region Метод добавляет новый элемент в начало очереди

        public void EnqueueFirst(T item)
        {

            // Увеличение размера внутреннего массива
            if (_items.Length == _size)
            {

                allocateNewArray(1);

            }

            if (_head > 0)
            {
                _head--;
            }
            else
            {
                // Оборот вокруг конца массива         
                _head = _items.Length - 1;
            }

            _items[_head] = item;

            _size++;
        }

        #endregion

        #region Метод добавляет новый элемент в конец очереди

        public void EnqueueLast(T item)
        {

            // Размер массива нужно увеличить 

            if (_items.Length == _size)
            {
                allocateNewArray(0);
            }

            // Обход массива

            if (_tail == _items.Length - 1)
            {
                _tail = 0;
            }

            else
            {
                _tail++;
            }

            _items[_tail] = item;
            _size++;
        }

        #endregion

        #region  DequeueFirst и DequeueLast

        //public T DequeueFirst()
        //{

        //}

        //public T DequeueLast()
        //{

        //}

        #endregion

        #region Метод возвращает первый элемент в очереди

        public T PeekFirst()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Двусвязная очередь пуста");
            }

            return _items[_head];
        }

        #endregion

        #region Метод возвращает последний элемент в очереди

        public T PeekLast()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Двусвязная очередь пуста");
            }

            return _items[_tail];
        }

        #endregion

        #region Свойство содержит количество элементов в двусвязной очереди

        public int Count
        {
            get
            {
                return _size;
            }
        }

        #endregion
    }
}
