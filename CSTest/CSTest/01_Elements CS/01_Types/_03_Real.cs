using System;
using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._01_Types
{
    [TestFixture]
    public class _03_Real
    {
        [Test]
        public void TestReal1()
        {
            #region ВЕЩЕСТВЕННЫЕ ТИПЫ

            // Вещественные типы предназначены для представления дробных чисел.

            // 32-bits = 4 bytes. ---------------------------------------------------------------------------

            // Вещественное со знаком. Диапазон - от +/-1.5 x 10^-45 до +/-3.4 x 10^38.
            float q = -0.123456789f;  // Указание суффикса f, является обязательным, 
            System.Single r = +1.123456789F; // т.к., компилятор интерпретирует данное число как double.


            // 64-bits = 8 bytes. ---------------------------------------------------------------------------

            // Вещественное со знаком. Диапазон - от +/-5.0 x 10^-324 до +/-1.7 x 10^308.
            double s = -0.123456789d;
            System.Double t = +1.123456789;

            #endregion
        }

        [Test]
        public void TestReal2()
        {
            float variable1 = 0.12345678901234567890f;
            double variable2 = 0.12345678901234567890d;
            decimal variable3 = 0.12345678901234567890m;

            Debug.WriteLine(variable1);
            Debug.WriteLine(variable2);
            Debug.WriteLine(variable3);
            /*
            0,1234568
            0,123456789012346
            0,12345678901234567890
            */
        }

        [Test]
        public void TestReal3Truncate()
        {
            double variable2 = 5566.92345678901234567890d;
            Debug.WriteLine(Math.Truncate(variable2));
            long variable3 = (long)variable2;
            Debug.WriteLine(variable3);
            /*
            5566
            */
        }
    }
}
