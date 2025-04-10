using NUnit.Framework;

namespace CSTest._01_Elements_CS._03_Operations
{
    
    [TestFixture]
    public class _01_Assignment
    {
        [Test]
        public void TestOperations1()
        {

            // ПРАВИЛО:
            // Все арифметические операции производимые над двумя значениями типа (byte, sbyte, short, ushort)
            // в качестве результата, возвращают значение типа int.

            // Присвоение со сложением для типа byte.
            byte variable1 = 0;

            //variable1 = variable1 + 5;       // ОШИБКА: Попытка неявного преобразования значения результата, тип int в тип byte.
            //variable1 = (byte)variable1 + 5; // ОШИБКА: Происходит преобразование типа byte в тип byte,  раньше выполнения операции сложения.

            variable1 = (byte)(variable1 + 5); // Громоздкое решение.

            variable1 += 5;                    // Элегантное решение.           

            //variable1 += 5000;               // Ошибка.  т.к. значение правой части выражения не должно превышать диапазон допустимых значений типа переменной

            // ПРАВИЛО:
            // Для типов int, uint, long и ulong, не происходит преобразования типа результата арифметических операций.

            #region Операции присвоения с...

            // Присвоение со сложением.
            int variable2 = 0;
            variable2 = variable2 + 5;
            variable2 += 5;

            // Присвоение с вычитанием.
            uint variable3 = 0;
            variable3 = variable3 - 5;
            variable3 -= 5;

            // Присвоение с умножением.
            long variable4 = 0;
            variable4 = variable4 * 5;
            variable4 *= 5;

            // Присвоение с делением.
            ulong variable5 = 0;
            variable5 = variable5 / 5;
            variable5 /= 5;

            // Присвоение остатка от деления.
            long variable6 = 0;
            variable6 = variable6 % 5;
            variable6 %= 5;

            #endregion

            // ПРАВИЛО:
            // Для типов float и double, не происходит преобразования типа результата арифметических операций.

            // Присвоение со сложением.
            float variable7 = 0;
            variable7 = variable7 + 5;
            variable7 += 5;

            // Присвоение с умножением.
            double variable8 = 0;
            variable8 = variable8 * 5;
            variable8 *= 5;
        }
    }
}
