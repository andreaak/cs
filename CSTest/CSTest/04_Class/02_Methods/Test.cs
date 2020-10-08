using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._04_Class._02_Methods
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestParameters1()
        {
            ChkNum ob = new ChkNum();
            for (int i = 2; i < 100; i++)
            {
                int factor;
                if (ob.IsPrime2(i, out factor))
                    Debug.WriteLine(i + " простое число.");
                else
                    Debug.WriteLine(i + " непростое число. " + factor);
            }




            _02_Parameters param = new _02_Parameters();
            param.DefaultParameters(5, prm: new []{1d,2d,3d});
            param.DefaultParameters(10, testStruct2: new TestStruct());
            
            param.DefaultParameters(20
                , 20
                , new TestStruct()
                , new TestStruct()
                , "PPP"
                , new object()
                , new TestClass()
                , new TestClass()
                , 20.0
                , 30.0);
            
            param.DefaultParameters(20
                , prm : null);
        }
    }

    class ChkNum
    {
        // Возвратить значение true, если значение 
        // параметра х окажется простым числом, 
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

        public bool IsPrime2(int x, out int factor)
        {
            factor = 0;
            if (x <= 1)
                return false;
            for (int i = 2; i < x / 2 + 1; i++)
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
