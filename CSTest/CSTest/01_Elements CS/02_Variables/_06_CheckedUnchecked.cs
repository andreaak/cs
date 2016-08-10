using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._02_Variables
{
    // Проверка переполнения - (checked)
    
    [TestFixture]
    public class _06_CheckedUnchecked
    {
        [Test]
        public void TestCheckedUnchecked1()
        {
            sbyte a = 127;

            // Проверять переполнение.
            checked
            {
                a++; // ОШИБКА System.OverflowException
            }

            // 127 + 1 = -128
            Debug.WriteLine(a);
        }

        // Комбинация проверки и запрета проверки переполнения.
        [Test]
        public void TestCheckedUnchecked2()
        {
            sbyte a = 126;
            sbyte b = 127;

            // Проверять переполнение.
            Debug.WriteLine("a = " + a);
            Debug.WriteLine("b = " + b);
            checked
            {
                a++;
                Debug.WriteLine("a = " + a);

                // Не проверять переполнение.
                unchecked
                {
                    b++;
                    Debug.WriteLine("b = " + b);
                }

                /*
                a = 126
                b = 127
                a = 127
                b = -128
                a = 127
                */

                a++; // ОШИБКА System.OverflowException
                Debug.WriteLine("a = " + a);
            }
        }
    }
}
