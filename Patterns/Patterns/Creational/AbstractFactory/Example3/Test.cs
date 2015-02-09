using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns.Creational.AbstractFactory.Example3
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
