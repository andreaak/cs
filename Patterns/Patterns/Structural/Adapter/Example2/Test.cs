using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns.Structural.Adapter.Example2
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
