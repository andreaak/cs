using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._02_Structural.Proxy._001_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Subject subject = new Proxy();
            subject.Request();
        }
    }
}
