using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._20_Algorithms
{
    [TestClass]
    public class SimpleNumber
    {
        [TestMethod]
        public void Test()
        {
            int temp = 10;
            Debug.WriteLine("Is simple number {0}: {1} ", temp, IsSimpleNumber(temp));
            temp = 11;
            Debug.WriteLine("Is simple number {0}: {1} ", temp, IsSimpleNumber(temp));
            temp = 19;
            Debug.WriteLine("Is simple number {0}: {1} ", temp, IsSimpleNumber(temp));
            temp = 21;
            Debug.WriteLine("Is simple number {0}: {1} ", temp, IsSimpleNumber(temp));
            /*
            Is simple number 10: False 
            Is simple number 11: True 
            Is simple number 19: True 
            Is simple number 21: False 
            */
        }

        public bool IsSimpleNumber(int n)
        {
            for (int i = 2; i < n / 2; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsPrime(int x, out int factor)
        {
            factor = 0;
            if (x <= 1)
                return false;
            for (int i = 2; i <= x / i; i++)
            {
                if ((x % i) == 0)
                {
                    factor = i;
                    return false;
                }
            }
            return true;
        }
    }
}
