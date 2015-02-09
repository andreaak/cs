using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithms.Deque
{
    public class DequeLinkedList<T>
    {
        LinkedList<T> _items = new LinkedList<T>();

        public void EnqueueFirst(T value)
        {
            _items.AddFirst(value);
        }

        public void EnqueueLast(T value)
        {
            _items.AddLast(value);
        }

        public T DequeueFirst()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Очередь пуста");
            }

            T temp = _items.First.Value;
            _items.RemoveFirst();

            return temp;
        }

        public T DequeueLast()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Очередь пуста"); //!!!!!
            }

            T temp = _items.Last.Value;
            _items.RemoveLast();
            return temp;
        }

        public T PeekFirst()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Очередь пуста");
            }

            return _items.First.Value;
        }

        public T PeekLast()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Очередь пуста");
            }
            return _items.Last.Value;
        }

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }


    }
}
