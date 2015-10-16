using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._01_Types
{
    [TestClass]
    public class _06_Char
    {
        [TestMethod]
        public void TestChar1()
        {
            #region СИМВОЛЬНЫЙ ТИП

            // 16-bits == 2 bytes. -------------------------------------------------------------------------

            // Символ в формате UNICODE.
            char y = 'a';
            System.Char z = 'A';

            #endregion
            
            char a = 'A';      // Символ
            char b = '\x0041'; // Значение в 16-ричном формате
            char c = '\u0041'; // Значение в формате unicode

            Debug.WriteLine(a);
            Debug.WriteLine(b);
            Debug.WriteLine(c);
        }
    }
}
