using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._01_Types
{
    [TestFixture]
    public class _04_Decimal
    {
        [Test]
        public void TestDecimal1()
        {
            #region ДЕСЯТИЧНЫЙ ТИП

            // 128-bits = 16 bytes. -----------------------------------------------------------------------

            // Десятичное со знаком. Диапазон - от от +/-1.0 x 10^-28 до +/-7.9 x 10^28.
            decimal u = -0.12345m;   // Указание суффикса m, является обязательным, 
            System.Decimal v = +1.1234567M; // т.к., компилятор интерпретирует данное число как double.
            Debug.WriteLine("Decimal = " + u);
            #endregion
        }
    }
}
