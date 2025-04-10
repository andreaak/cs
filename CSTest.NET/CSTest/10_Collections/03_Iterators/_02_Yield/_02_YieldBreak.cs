using System.Collections;
using System.Diagnostics;

namespace CSTest._10_Collections._03_Iterators._02_Yield
{
    class _02_YieldBreak
    {
        char ch = 'A';
        // Этот итератор возвращает первые 10 букв английского алфавита, 
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < 26; i++)
            {
                if (i == 10)
                {
                    Debug.WriteLine("yield break");
                    yield break; // прервать итератор преждевременно 
                }
                Debug.WriteLine("yield return");
                yield return (char)(ch + i);
            }
        }
    }
}
