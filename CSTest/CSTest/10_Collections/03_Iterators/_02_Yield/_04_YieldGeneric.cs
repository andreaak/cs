using System.Collections.Generic;

namespace CSTest._10_Collections._03_Iterators._02_Yield
{
    class _04_YieldGeneric<T>
    {
        T[] array;
        public _04_YieldGeneric(T[] a)
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