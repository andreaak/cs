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
            Debug.WriteLine("yield return");
            yield return 'В';
            Debug.WriteLine("yield return");
            yield return 'C';
            Debug.WriteLine("yield return");
            yield return 'D';
            Debug.WriteLine("yield return");
            yield return 'Е';
            Debug.WriteLine("yield return last");
        }
    }
}
