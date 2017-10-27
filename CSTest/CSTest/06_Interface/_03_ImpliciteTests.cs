using System.Diagnostics;
using CSTest._06_Interface._0_Setup;
using NUnit.Framework;

namespace CSTest._06_Interface
{
    [TestFixture]
    public class _03_ImpliciteTests
    {
        [Test]
        public void TestInterfaceImplicite1()
        {
            _03_ImpliciteMethods instance = new _03_ImpliciteMethods();
            //instance.Method();
            instance.Method1();
            instance.Method2();

            _031_Interface instance1 = instance;
            instance1.Method();
            instance1.Method1();

            _032_Interface instance2 = instance;
            instance2.Method();
            instance2.Method2();

            /*
            _03_ImpliciteMethods.Method1
            _03_ImpliciteMethods.Method2
            _031_Interface.Method
            _03_ImpliciteMethods.Method1
            _032_Interface.Method
            _03_ImpliciteMethods.Method2
            */
        }
    }
}
