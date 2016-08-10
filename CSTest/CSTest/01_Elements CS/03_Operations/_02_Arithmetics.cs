using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._03_Operations
{
    [TestFixture]
    public class _02_Arithmetics
    {
        [Test]
        public void TestArithmetics1()
        {
            // Addition (+) 
            byte summand1 = 1, summand2 = 2; // Множественное объявление.
            int sum = 0;
            sum = summand1 + summand2;

            Debug.WriteLine(sum);

            // Subtraction (-)
            byte minuend = 5, subtrahend = 3;
            int difference = 0;
            difference = minuend - subtrahend;

            Debug.WriteLine(difference);

            // Multiplication (*)
            byte factor1 = 2, factor2 = 3;
            int product = 0;
            product = factor1 * factor2;

            Debug.WriteLine(product);

            // Division (/)
            byte dividend = 5, divisor = 2;
            int quotient = 0, remainder = 0;
            quotient = dividend / divisor;

            Debug.WriteLine(quotient);

            // Remainder after division (%)
            remainder = dividend % divisor;

            Debug.WriteLine(remainder);
        }

        [Test]
        public void TestArithmetics1IncDec()
        {
            Debug.WriteLine("----- Постфиксный инкремент"); // Post-increment

            byte number1 = 0;
            Debug.WriteLine(number1++); // Сначала выводим на экран, потом увеличиваем на 1.
            Debug.WriteLine(number1);

            Debug.WriteLine("----- Префиксный инкремент"); // Pre-increment

            byte number2 = 0;
            Debug.WriteLine(++number2); // Сначала увеличиваем на 1, потом выводим на экран.

            Debug.WriteLine("----- Постфиксный декремент"); // Post-decrement

            sbyte number3 = 0;
            Debug.WriteLine(number3--); // Сначала выводим на экран, потом уменьшаем на 1.
            Debug.WriteLine(number3);

            Debug.WriteLine("----- Префиксный декремент"); // Pre-decrement

            sbyte number4 = 0;
            Debug.WriteLine(--number4); // Сначала уменьшаем на 1, потом выводим на экран.

            /*
            ----- Постфиксный инкремент
            0
            1
            ----- Префиксный инкремент
            1
            ----- Постфиксный декремент
            0
            -1
            ----- Префиксный декремент
            -1
             */
        }
    }
}
