using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns.Creational.AbstractFactory.Example2
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Client client = null;

            client = new Client(new CocaColaFactory());
            client.Run();

            client = new Client(new PepsiFactory());
            client.Run();
        }
    }
}
