using NUnit.Framework;

namespace Patterns._01_Creational.AbstractFactory._006_BaseModified
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Client client = new Client(Factory.ConcreteFactory1);
            client.Run();

            client = new Client(Factory.ConcreteFactory2);
            client.Run();

            // Задержка.
            //Debug.ReadKey();
        }
    }
}
