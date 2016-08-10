using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._03_Operations
{
    [TestFixture]
    public class _03_Comparison
    {
        // Операции сравнения и проверки на равенство (<, <=, >, >=, ==, !=)
        [Test]
        public void TestComparison1()
        {
            byte value1 = 0, value2 = 1;
            bool result = false;

            // Less than
            result = value1 < value2;
            Debug.WriteLine(result);

            // Greater than
            result = value1 > value2;
            Debug.WriteLine(result);

            // Less than or equal to
            result = value1 <= value2;
            Debug.WriteLine(result);

            // Greater than or equal to
            result = value1 >= value2;
            Debug.WriteLine(result);

            // Equals
            result = value1 == value2;
            Debug.WriteLine(result);

            // Not equals
            result = value1 != value2;
            Debug.WriteLine(result);
        }

        // Сравнение значений разных типов.
        [Test]
        public void TestComparison2()
        {
            bool result = false;

            int a = 1;
            float b = 2.0f;
            result = a < b;   // Сравнение значения типа int, со значением типа float - допустимо.

            string c = "Hello";
            //result = c < a; // Сравнение значения типа int, со значением типа string - не допустимо.
        }
    }

}
