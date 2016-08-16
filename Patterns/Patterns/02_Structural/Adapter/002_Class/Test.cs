using NUnit.Framework;

namespace Patterns._02_Structural.Adapter._002_Class
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            ITarget target = new Adapter();
            target.Request();
        }
    }
}
