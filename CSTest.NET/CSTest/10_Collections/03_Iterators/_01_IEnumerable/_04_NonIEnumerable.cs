using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSTest._10_Collections._03_Iterators._01_IEnumerable
{
    // Реализовать интерфейсы IEnumerable<char> и IEnumerator<char>. 
    class _04_NonIEnumerable 
    {
        char[] chrs = { 'А', 'В', 'C', 'D' };
        int position = -1;

        //// Реализовать интерфейс IEnumerable<char>. 
        public _04_NonIEnumerable GetEnumerator()
        {
            Debug.WriteLine("GetEnumerator");
            return this;
        }

        public char Current
        {
            get
            {
                return chrs[position];
            }
        }

        // Перейти к следующему объекту
        public bool MoveNext()
        {
            Debug.WriteLine("MoveNext");
            if (position == chrs.Length - 1)
            {
                return false;
            }
            position++;

            return true;
        }

        // Установить перечислитель в начало
        //public void Reset()
        //{
        //    Debug.WriteLine("Reset");
        //    position = -1;
        //}

        //public void Dispose()
        //{
        //    Debug.WriteLine("Dispose");
        //}
    }
}