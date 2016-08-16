using NUnit.Framework;

namespace Patterns._02_Structural.Adapter._003_Object
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Target target = new Adapter();
            target.Request();
        }
    }
}
