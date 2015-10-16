using System.Collections;
using System.Diagnostics;

namespace CSTest._10_Collections.Iterators
{
    /*
    Ключевое слово yield сообщает компилятору, что метод, в котором оно содержится, 
    является блоком итератора. Для реализации поведения, определенного в блоке итератора, 
    компилятор создает класс. 
    В блоке итератора ключевое слово yield используется совместно с ключевым словом return 
    для предоставления значения объекту перечислителя, например значения, 
    возвращаемого в каждом цикле оператора foreach. 
    Ключевое слово yield всегда используется вместе с ключевым словом break для обозначения конца итерации. 
    Оператор yield не может использоваться в анонимных методах. 
    */
    class _04_Yield
    {
        char ch1 = 'A';
        char[] chrs = { 'A', 'B', 'C', 'D' };
        // Этот итератор возвращает символы из массива chrs. 
        public IEnumerator GetEnumerator()
        {
            foreach (char ch in chrs)
            {
                Debug.WriteLine("yield return");
                yield return ch;
            }
        }

        public IEnumerable IterateEnumerable()
        {
            foreach (char ch in chrs)
            {
                Debug.WriteLine("yield return");
                yield return ch;
            }
        }

        public IEnumerable MyItr(int end)
        {
            for (int i = 0; i < end; i++)
            {
                yield return (char)(ch1 + i);
            }
        }
        // Этот итератор возвращает буквы в заданных пределах, 
        public IEnumerable MyItr(int begin, int end)
        {
            for (int i = begin; i < end; i++)
            {
                yield return (char)(ch1 + i);
            }
        }
    }
}
