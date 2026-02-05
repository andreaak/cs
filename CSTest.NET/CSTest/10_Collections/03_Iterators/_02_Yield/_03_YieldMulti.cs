using System.Collections;
using System.Diagnostics;

namespace CSTest._10_Collections._03_Iterators._02_Yield
{
    class _03_YieldMulti
    {
        // Этот итератор возвращает буквы А, В, С, D и Е. 
        public IEnumerator GetEnumerator()
        {
            Debug.WriteLine("GetEnumerator");
            yield return 'A';
            Debug.WriteLine("yield return 1");
            yield return 'В';
            Debug.WriteLine("yield return 2");
            yield return 'C';
            Debug.WriteLine("yield return 3");
            yield return 'D';
            Debug.WriteLine("yield return 4");
            yield return 'Е';
            Debug.WriteLine("yield return last");
        }
    }
}
