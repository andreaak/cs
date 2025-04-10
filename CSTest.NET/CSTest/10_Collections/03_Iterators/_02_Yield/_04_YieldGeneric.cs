using System.Collections.Generic;
using System.Diagnostics;

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
                Debug.WriteLine("yield return" + obj);
                yield return obj;
            }
        }

        //public IEnumerator<T> IterateEnumerator()
        //{
        //    foreach (T obj in array)
        //    {
        //        Debug.WriteLine("yield return" + obj);
        //        yield return obj;
        //    }
        //}

        public IEnumerable<T> IterateEnumerable(int end)
        {
            for (int i = 0; i < end; i++)
            {
                Debug.WriteLine("yield return " + array[i]);
                yield return array[i];
            }
        }

        public IEnumerable<T> IterateEnumerableAll(int end)
        {
            List<T> list = new List<T>();

            for (int i = 0; i < end; i++)
            {
                Debug.WriteLine("Value " + array[i]);
                list.Add(array[i]);
            }

            return list;
        }

        // Этот итератор возвращает буквы в заданных пределах, 
        public IEnumerable<T> IterateEnumerable(int begin, int end)
        {
            for (int i = begin; i < end; i++)
            {
                yield return array[i];
            }
        }
    }
}