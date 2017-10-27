using CSTest._06_Interface._0_Setup;
using NUnit.Framework;

namespace CSTest._06_Interface
{
    [TestFixture]
    public class _05_InterfaceUnionTests
    {
        [Test]
        public void TestInterface1()
        {
            _05_InterfaceUnion instance = new _05_InterfaceUnion();
            instance.Method();

            _051_IInterface instance1 = instance;
            instance1.Method();

            _052_IInterface instance2 = instance;
            instance2.Method();
            /*
            _05_InterfaceUnion.Method
            _05_InterfaceUnion.Method
            _05_InterfaceUnion.Method 
            */
        }
    }
}
