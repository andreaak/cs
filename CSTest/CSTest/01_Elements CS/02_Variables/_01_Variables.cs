using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._02_Variables
{
    [TestFixture]
    public class _01_Variables
    {
        [Test]
        public void TestVariables1()
        {
            // Переменная (Variable) - это область памяти, которая хранит в себе некоторое значение, которое можно изменить.

            // Тип данных C# (псевдоним)
            byte a = 2;

            // Системный тип данных
            System.Byte b = 2;

            // Вывод значений переменных на экран.
            Debug.WriteLine(a);
            Debug.WriteLine(b);

            // cоздаем переменную с именем a, типа byte и не присваиваем ей никакого значения.

            byte c;
            bool d;
            char e;
            string s;

            // ОШИБКА: Запрещается использование неинициализированной локальной переменной!
            //Debug.WriteLine(c);

            //int bool = 5;      // Illegal
            int @bool = 7;       // Legal
            Debug.WriteLine(@bool);

            // Символ @ не является частью идентификатора, поэтому, @myVariable - это тоже самое, что и myVariable.
            string @myVariable = "Hello";
            Debug.WriteLine(myVariable);
        }

        // Использование локальных областей и локальных переменных.
        [Test]
        public void TestVariables2()
        {
            // ПРАВИЛО:
            // В коде можно создавать локальные области и в двух разных локальных областях хранить одноименные переменные.

            // Локальная область 1
            {
                int a = 1;
                Debug.WriteLine(a);
            }

            // Локальная область 2
            {
                int a = 2;
                Debug.WriteLine(a);
            }


            // ПРАВИЛО:
            // Если в коде имеются локальные области, то запрещается хранить одноименные переменные за пределами локальных областей.

            //int a = 3; // ОШИБКА: Переменная с таким именем уже существует в локальной области.
        }
    }
}
