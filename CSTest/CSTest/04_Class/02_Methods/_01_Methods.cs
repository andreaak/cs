using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._04_Class._02_Methods
{
    class _01_Methods
    {
        // Методы, которые возвращают логическое значение, называют методами-предикатами.
        static bool And(bool a, bool b)
        {
            return a && b;
        }

        // Пример правильного множественного возврата из метода.
        static string Compare(int value1, int value2)
        {
            if (value1 < value2)
            {
                return "Comparison Less Then";
            }
            else if (value1 > value2)
            {
                return "Comparison Greater Then";
            }

            return "Comparison Equal";
        }

        // Пример: Использование сторожевого оператора, для защиты номинального варианта.
        static string Function(string name)
        {
            // Выполняем проверку. При обнаружении ошибок завершаем работу.

            if (name == "fool")   // Сторожевой оператор.
            {
                return "Вы использовали недопустимое слово.";
            }

            // Код номинального варианта.

            string sentence = "Hello " + name + "!";

            return sentence;
        }
    }
}
