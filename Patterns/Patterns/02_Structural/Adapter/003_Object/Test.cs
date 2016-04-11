using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns.Structural.Adapter._003_Object
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Target target = new Adapter();
            target.Request();
        }
    }
}
