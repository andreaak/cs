using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSTest._10_Collections._03_Iterators._01_IEnumerable
{
    // Реализовать интерфейсы IEnumerable<char> и IEnumerator<char>. 
    class _03_IEnumerableGeneric : IEnumerator<char>, IEnumerable<char>
    {
        char[] chrs = { 'А', 'В', 'C', 'D' };
        int position = -1;

        // Реализовать интерфейс IEnumerable<char>. 
        public IEnumerator<char> GetEnumerator()
        {
            Debug.WriteLine("GetEnumerator");
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Debug.WriteLine("IEnumerable.GetEnumerator");
            return GetEnumerator();
        }

        // Реализовать интерфейс IEnumerator<char>. 
        // Возвратить текущий объект
        public char Current
        {
            get
            {
                return chrs[position];
            }
        }
        object IEnumerator.Current
        {
            get { return Current; }
        }

        // Перейти к следующему объекту
        public bool MoveNext()
        {
            Debug.WriteLine("MoveNext");
            if (position == chrs.Length - 1)
            {
                Reset(); //установить перечислитель в конец 
                return false;
            }
            position++;
            return true;
        }

        // Установить перечислитель в начало
        public void Reset()
        {
            Debug.WriteLine("Reset");
            position = -1;
        }

        public void Dispose()
        {
            Debug.WriteLine("Dispose");
        }
    }
}