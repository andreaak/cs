using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithms.Queue
{
    /// <summary>
    /// A collection that returns the highest priority item first and lowest priority item last.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the collection</typeparam>
    public class PriorityQueue<T> : System.Collections.Generic.IEnumerable<T>
        where T : IComparable<T>
    {
        System.Collections.Generic.LinkedList<T> _items =
            new System.Collections.Generic.LinkedList<T>();

        /// <summary>
        /// Adds an item to the queue in priority order
        /// </summary>
        /// <param name="item">The item to place in the queue</param>
        public void Enqueue(T item)
        {
            // if the list is empty, just add the item
            if (_items.Count == 0)
            {
                _items.AddLast(item);
            }
            else
            {
                // find the proper insert point
                var current = _items.First;

                // while we're not at the end of the list and the current value
                // is larger than the value being inserted...
                while (current != null && current.Value.CompareTo(item) >= 0)
                {
                    current = current.Next;
                }

                if (current == null)
                {
                    // we made it to the end of the list
                    _items.AddLast(item);
                }
                else
                {
                    // the current item is <= the one being added
                    // so add the item before it.
                    _items.AddBefore(current, item);
                }
            }
        }

        /// <summary>
        /// Removes and returns the highest priority item from the queue
        /// </summary>
        /// <returns>The front item from the queue</returns>
        public T Dequeue()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            // store the last value in a temporary variable
            T value = _items.First.Value;

            // remove the last item
            _items.RemoveFirst();

            // return the stored last value
            return value;
        }

        /// <summary>
        /// Returns the highest priority item from the queue without removing it from the queue
        /// </summary>
        /// <returns>The front item from the queue</returns>
        public T Peek()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _items.First.Value;
        }

        /// <summary>
        /// The number of items in the queue
        /// </summary>
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        /// <summary>
        /// Removes all items from the queue
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        /// <summary>
        /// Returns an enumerator that enumerates the queue
        /// </summary>
        /// <returns>The enumerator</returns>
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that enumerates the queue
        /// </summary>
        /// <returns>The enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
