using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._20_Algorithms
{
    [TestClass]
    public class Factorial
    {
        [TestMethod]
        public void Test()
        {
            int temp = 10;
            int factorial = Factorial1(temp);
            Debug.WriteLine("Factorial recursive " + temp + " = " + factorial);
            factorial = factorial2(temp);
            Debug.WriteLine("Factorial iterative " + temp + " = " + factorial);
            /*
            Factorial recursive 10 = 3628800
            Factorial iterative 10 = 3628800
            */
        }

        public int Factorial1(int n)
        {
            if (n <= 1)
            {
                return 1;
            }
            return n * Factorial1(n - 1);
        }

        public int factorial2(int n)
        {
            int s = 1;
            for (int i = n; i > 0; i--)
            {
                s *= i;
            }
            return s;
        }
    }
}
