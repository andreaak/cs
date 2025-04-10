using System.Collections;
using System.Diagnostics;

namespace CSTest._10_Collections._03_Iterators._01_IEnumerable
{
    // Реализовать интерфейсы IEnumerator. 
    class _02_IEnumerable : IEnumerator
    {
        char[] chrs = { 'А', 'В', 'C', 'D' };
        int position = -1;
        // Реализовать интерфейс IEnumerable. 
        public IEnumerator GetEnumerator()
        {
            Debug.WriteLine("GetEnumerator");
            return this;
        }
        // В следующих методах реализуется интерфейс IEnumerator 
        // Возвратить текущий объект, 
        public object Current
        {
            get
            {
                return chrs[position];
            }
        }
        // Перейти к следующему объекту, 
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
        // Установить перечислитель в начало, 
        public void Reset()
        {
            Debug.WriteLine("Reset");
            position = -1;
        }
    }
}
