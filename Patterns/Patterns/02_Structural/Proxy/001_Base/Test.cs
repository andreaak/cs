using NUnit.Framework;

namespace Patterns._02_Structural.Proxy._001_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Subject subject = new Proxy();
            subject.Request();
        }
    }
}
