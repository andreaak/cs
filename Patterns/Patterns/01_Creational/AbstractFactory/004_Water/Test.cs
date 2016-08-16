using NUnit.Framework;
using Patterns._01_Creational.AbstractFactory._004_Water.AbstractFactory;

namespace Patterns._01_Creational.AbstractFactory._004_Water
{
    [TestFixture]
    public class Test
    {
        [Test]
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
