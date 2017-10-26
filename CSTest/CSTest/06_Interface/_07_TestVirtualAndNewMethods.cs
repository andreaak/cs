using NUnit.Framework;

namespace CSTest._06_Interface
{
    [TestFixture]
    public class _07_TestVirtualAndNewMethods
    {
        [Test]
        public void TestInterface9()
        {
            _071_DerivedClass instance = new _071_DerivedClass();
            instance.Method();
            instance.MethodNew();
            instance.MethodVirtual();

            _071_IInterface instance1 = instance;
            instance1.Method();

            /*
            _07_BaseClass.Method()
            _071_DerivedClass.MethodNew()
            _071_DerivedClass.MethodVirtual()
            _07_BaseClass.Method()
            */
        }

        [Test]
        public void TestInterface10()
        {
            _071_DerivedClass instance = new _071_DerivedClass();
            instance.Method();
            instance.MethodNew();
            instance.MethodVirtual();

            _072_IInterface instance1 = instance;
            instance1.MethodNew();
            instance1.MethodVirtual();
            /*
            _07_BaseClass.Method()
            _071_DerivedClass.MethodNew()
            _071_DerivedClass.MethodVirtual()
            _07_BaseClass.MethodNew()
            _071_DerivedClass.MethodVirtual()
            */
        }

        [Test]
        public void TestInterface11()
        {
            _072_DerivedClass instance = new _072_DerivedClass();
            instance.Method();
            instance.MethodNew();
            instance.MethodVirtual();

            _072_IInterface instance1 = instance;
            instance1.MethodNew();
            instance1.MethodVirtual();
            /*
            _07_BaseClass.Method()
            _072_DerivedClass.MethodNew()
            _072_DerivedClass.MethodVirtual()
            _072_DerivedClass.MethodNew()
            _072_DerivedClass.MethodVirtual()
            */
        }
    }
}
