using CSTest._06_Interface._0_Setup;
using NUnit.Framework;

namespace CSTest._06_Interface
{
    [TestFixture]
    public class _06_AbstractClassAndInterfaceTests
    {
        [Test]
        public void TestInterface8()
        {
            _06_RealClass instance = new _06_RealClass();
            instance.Method();
            instance.Method2();
        }
    }
}
