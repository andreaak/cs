using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Creational.FactoryMethod.Example10
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            ICreator creator = new StandardCreator();

            IProduct productA = creator.CreateProduct<ProductA>();
            IProduct productB = creator.CreateProduct<ProductB>();
            IProduct productC = creator.CreateProduct<ProductC>();

            // Задержка.
            //Console.ReadKey();
        }

    }
}
