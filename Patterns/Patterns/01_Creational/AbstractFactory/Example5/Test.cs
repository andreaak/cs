using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns.Creational.AbstractFactory.Example5
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Client client = new Client(Factory.ConcreteFactory1);
            client.Run();

            client = new Client(Factory.ConcreteFactory2);
            client.Run();

            // Задержка.
            //Console.ReadKey();
        }
    }
}
