using CSTest._06_Interface._0_Setup;
using NUnit.Framework;

namespace CSTest._06_Interface
{
    [TestFixture]
    public class _01_Test
    {
        [Test]
        public void TestInterface1()
        {
            _01_Base my = new _01_Base();

            my.Method();
        }
    }
}
