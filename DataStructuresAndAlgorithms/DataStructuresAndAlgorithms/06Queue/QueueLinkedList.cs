using System;
using System.Collections.Generic;

namespace DataStructuresAndAlgorithms.Queue
{
    public class QueueLinkedList<T>
    {
        LinkedList<T> _items = new LinkedList<T>();

        // Метод добавляет новый элемент в конец очереди

        public void Enqueue(T value)
        {
            _items.AddFirst(value);
        }

        // Метод удаляет первый элемент из очереди

        public T Dequeue()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Очередь пуста");
            }

            T last = _items.Last.Value;
            _items.RemoveLast();
            return last;
        }

        // Метод возвращает первый элемент очереди

        public T Peek()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Очередь пуста");
            }

            return _items.Last.Value;
        }

        // Свойство хранит в себе количество элементов очереди

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }
    }
}
