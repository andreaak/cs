using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._02_Structural.Adapter._002_Class
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            ITarget target = new Adapter();
            target.Request();
        }
    }
}
