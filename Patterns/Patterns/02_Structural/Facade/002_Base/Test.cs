using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._02_Structural.Facade._002_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Facade facade = new Facade();
            facade.OperationAB();
            facade.OperationBC();
        }
    }
}
