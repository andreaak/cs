using System;
using System.Collections.Generic;

namespace DataStructuresAndAlgorithms.Stack
{
    public class StackLinkedList<T>
    {
        LinkedList<T> _items = new LinkedList<T>();

        public void Push(T value)
        {
            _items.AddLast(value);
        }

        public T Pop()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }

            T result = _items.Last.Value;
            _items.RemoveLast();

            return result;
        }

        public T Peek()
        {
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
