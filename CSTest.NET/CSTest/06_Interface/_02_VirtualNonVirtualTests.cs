using System.Diagnostics;
using CSTest._06_Interface._0_Setup;
using NUnit.Framework;

namespace CSTest._06_Interface
{
    [TestFixture]
    public class _02_VirtualNonVirtualTests
    {
        [Test]
        public void TestInterface1Base()
        {
            _02_BaseClass baseInstance = new _02_BaseClass();
            baseInstance.Method();
            baseInstance.MethodNew();
            baseInstance.MethodVirtual();

            _02_IVirtualNonVirtual instance = baseInstance;
            instance.MethodNew();
            instance.MethodVirtual();

            _021_IVirtualNonVirtual instance21 = baseInstance;
            instance21.Method();

            /*
            _02_BaseClass.Method
            _02_BaseClass.MethodNew
            _02_BaseClass.MethodVirtual
            _02_BaseClass.MethodNew
            _02_BaseClass.MethodVirtual
            _02_BaseClass.Method
            */
        }

        [Test]
        public void TestInterface2Derived()
        {
            _02_BaseClass baseInstance = new _02_DerivedClass();
            baseInstance.Method();
            baseInstance.MethodNew();
            baseInstance.MethodVirtual();
            Debug.WriteLine("");

            _02_IVirtualNonVirtual instance = baseInstance;
            instance.MethodNew();
            instance.MethodVirtual();
            Debug.WriteLine("");

            _021_IVirtualNonVirtual instance21 = baseInstance;
            instance21.Method();
            Debug.WriteLine("");

            _02_DerivedClass derivedInstance = new _02_DerivedClass();
            derivedInstance.Method();
            derivedInstance.MethodNew();
            derivedInstance.MethodVirtual();
            Debug.WriteLine("");

            instance = derivedInstance;
            instance.MethodNew();
            instance.MethodVirtual();
            Debug.WriteLine("");

            instance21 = derivedInstance;
            instance21.Method();

            /*
            _02_BaseClass.Method
            _02_BaseClass.MethodNew
            _02_DerivedClass.MethodVirtual
            Field1: 7

            _02_DerivedClass.MethodNew
            Field1: 7
            _02_DerivedClass.MethodVirtual
            Field1: 7

            _02_BaseClass.Method

            _02_DerivedClass.Method
            _02_DerivedClass.MethodNew
            Field1: 7
            _02_DerivedClass.MethodVirtual
            Field1: 7

            _02_DerivedClass.MethodNew
            Field1: 7
            _02_DerivedClass.MethodVirtual
            Field1: 7

            _02_BaseClass.Method
            */
        }

        [Test]
        public void TestInterface3Derived()
        {
            _02_BaseClass baseInstance = new _021_DerivedClass();
            baseInstance.Method();
            baseInstance.MethodNew();
            baseInstance.MethodVirtual();
            Debug.WriteLine("");

            _02_IVirtualNonVirtual instance = baseInstance;
            instance.MethodNew();
            instance.MethodVirtual();
            Debug.WriteLine("");

            _021_IVirtualNonVirtual instance21 = baseInstance;
            instance21.Method();
            Debug.WriteLine("");

            _021_DerivedClass derivedInstance = new _021_DerivedClass();
            derivedInstance.Method();
            derivedInstance.MethodNew();
            derivedInstance.MethodVirtual();
            Debug.WriteLine("");

            instance = derivedInstance;
            instance.MethodNew();
            instance.MethodVirtual();
            Debug.WriteLine("");

            instance21 = derivedInstance;
            instance21.Method();

            /*
            _02_BaseClass.Method
            _02_BaseClass.MethodNew
            _02_DerivedClass.MethodVirtual
            Field1: 8

            _02_BaseClass.MethodNew
            _02_DerivedClass.MethodVirtual
            Field1: 8

            _02_BaseClass.Method

            _02_DerivedClass.Method
            _02_DerivedClass.MethodNew
            Field1: 8
            _02_DerivedClass.MethodVirtual
            Field1: 8

            _02_BaseClass.MethodNew
            _02_DerivedClass.MethodVirtual
            Field1: 8

            _02_BaseClass.Method
            */
        }
    }
}
