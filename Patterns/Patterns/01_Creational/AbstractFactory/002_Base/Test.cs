using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns.Creational.AbstractFactory._002_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Client client = null;

            client = new Client(new ConcreteFactory1());
            client.Run();

            client = new Client(new ConcreteFactory2());
            client.Run();
        }
    }
}
