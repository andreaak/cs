using NUnit.Framework;

namespace Patterns._02_Structural.Facade._002_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Facade facade = new Facade();
            facade.OperationAB();
            facade.OperationBC();
        }
    }
}
