using NUnit.Framework;
using Patterns._01_Creational.AbstractFactory._002_Base.AbstractFactory;

namespace Patterns._01_Creational.AbstractFactory._002_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
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
