using CSTest._06_Interface._0_Setup;
using NUnit.Framework;

namespace CSTest._06_Interface
{
    [TestFixture]
    public class _04_InheritanceTests
    {
        [Test]
        public void TestInterface1Inheritance()
        {
            _041_InterfaceInheritance instance = new _041_InterfaceInheritance();
            instance.Method1();
            instance.Method2();

            _41_1IInterface instance1 = instance;
            instance1.Method1();

            _41_2IInterface instance2 = instance;
            instance2.Method1();
            instance2.Method2();
            /*
            _041_InterfaceInheritance.Method1
            _041_InterfaceInheritance.Method2
            _041_InterfaceInheritance.Method1
            _041_InterfaceInheritance.Method1
            _041_InterfaceInheritance.Method2
            */
        }

        [Test]
        public void TestInterface2InheritanceSameMethods()
        {
            _042_InterfaceInheritanceSameMethods instance = new _042_InterfaceInheritanceSameMethods();
            instance.Method();

            _042_1_IInterface instance1 = instance;
            instance1.Method();

            _042_2_IInterface instance2 = instance;
            instance2.Method();

            /*
            _042_InterfaceInheritanceSameMethods.Method
            IInterface42_1.Method
            IInterface42_2.Method
            */
        }
    }
}
