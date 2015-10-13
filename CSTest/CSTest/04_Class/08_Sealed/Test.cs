using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._04_Class._08_Sealed
{

    
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestSealed1()
        {
            SealedClass instance = new SealedClass();
            instance.x = 100;
            instance.y = 200;

            Debug.WriteLine("x = {0}, y = {1}", instance.x, instance.y);
        }

        [TestMethod]
        public void TestSealed2()
        {
            ClassA instanceA = new ClassA();
            instanceA.Method1();
            instanceA.Method2();

            Debug.WriteLine(new string('-', 15));

            ClassB instanceB = new ClassB();
            instanceB.Method1();
            instanceB.Method2();

            Debug.WriteLine(new string('-', 15));

            ClassC instanceC = new ClassC();
            instanceC.Method1();
            instanceC.Method2();
        }
    }
}
