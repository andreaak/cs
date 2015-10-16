using System.Collections.Generic;

namespace CSTest._10_Collections.Iterators
{
    class _07_YieldGeneric<T>
    {
        T[] array;
        public _07_YieldGeneric(T[] a)
        {
            array = a;
        }
        // Этот итератор возвращает символы из массива array. 
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T obj in array)
            {
                yield return obj;
            }
        }
        public IEnumerable<T> IterateEnumerable()
        {
            foreach (T obj in array)
            {
                yield return obj;
            }
        }
    }
}