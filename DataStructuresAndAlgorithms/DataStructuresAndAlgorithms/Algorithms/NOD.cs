using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Algorithms
{
    [TestClass]
    public class NOD
    {
        [TestMethod]
        public void Test()
        {
            int a = 7, b = 3;
            Debug.WriteLine("Number1 {0} Number2 {1} Nod {2}", a, b, Nod(a, b));
            a = 12;
            b = 8;
            Debug.WriteLine("Number1 {0} Number2 {1} Nod {2}", a, b, Nod(a, b));
            a = 45;
            b = 25;
            Debug.WriteLine("Number1 {0} Number2 {1} Nod {2}", a, b, Nod(a, b));
            /*
            Number1 7 Number2 3 Nod 1
            Number1 12 Number2 8 Nod 4
            Number1 45 Number2 25 Nod 5
            */
        }
        /*
        Идея алгоритма от большего меньшее пока числа не станут равны.
        Полученное число и является наибольшим общим делителем
        20 и 15     20 - 15 = 5
        5 и 15      15 - 5 = 10
        5 и 10      10 - 5 = 5
        5 и 5       5 == 5 НОД
        */
        public long Nod(long a, long b)
        {
            if (a == b)
            {
                return a;
            }
            if (a > b)
            {
                return Nod(a - b, b);
            }
            else
            {
                return Nod(b - a, a);
            }
        }
    }
}
