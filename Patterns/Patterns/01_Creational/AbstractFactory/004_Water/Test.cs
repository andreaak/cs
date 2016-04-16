using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._01_Creational.AbstractFactory._004_Water.AbstractFactory;

namespace Patterns._01_Creational.AbstractFactory._004_Water
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
