using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._02_Variables
{
    // Оператор sizeof - позволяет получить размер значения в байтах для указанного типа.
    // Оператор sizeof можно применять только к типам: 
    // byte, sbyte, short, ushort, int, uint, long, ulong, float, double, decimal, char, bool.
    // Возвращаемые оператором sizeof значения имеют тип int.
    
    [TestClass]
    public class _03_sizeof
    {
        [TestMethod]
        public void TestSizeof()
        {
            int doubleSize = sizeof(double);
            Debug.WriteLine("Размер типа double: {0} байт.", doubleSize);

            Debug.WriteLine("Размер типа int: {0} байт.", sizeof(int));
            Debug.WriteLine("Размер типа bool: {0} байт.", sizeof(bool));
            Debug.WriteLine("Размер типа long: {0} байт.", sizeof(long));
            Debug.WriteLine("Размер типа short: {0} байт.", sizeof(short));
        }
    }
}
