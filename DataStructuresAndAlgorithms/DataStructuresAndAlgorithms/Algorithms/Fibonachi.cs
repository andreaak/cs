using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._20_Algorithms
{
    [TestClass]
    public class Fibonachi
    {
        [TestMethod]
        public void Test()
        {
            int temp = 7;
            Debug.WriteLine("Fibonachi {0}: {1} ", temp, GetFibonachi(temp));
            temp = 9;
            Debug.WriteLine("Fibonachi {0}: {1} ", temp, GetFibonachi(temp));
            temp = 20;
            Debug.WriteLine("Fibonachi {0}: {1} ", temp, GetFibonachi(temp));
            /*
            Fibonachi 7: 13 
            Fibonachi 9: 34 
            Fibonachi 20: 6765 
            */
        }
        /*
        Последовательность Фибоначчи начинается с двух единиц 
        а далее каждый последующий элемент равен сумме двух предидущих
        1 1 2 3 5 8 13
        */
        public long GetFibonachi(int n)
        {
            if (n <= 2)
            {
                return 1;
            }
            return GetFibonachi(n - 1) + GetFibonachi(n - 2);
        }

        public long GetFibonachi2(uint n)
        {
            int[] result = { 1, 1 };
            if (n < 2)
            {
                return result[n];
            }

            long fibNMinusOne = 1;
            long fibNMinusTwo = 1;
            long res = 0;

            for (uint i = 2; i <= n; ++i)
            {
                res = fibNMinusOne + fibNMinusTwo;
                fibNMinusTwo = fibNMinusOne;
                fibNMinusOne = res;
            }
            return res;
        }
    }
}
