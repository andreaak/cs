using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Creational.FactoryMethod._010_Generic
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
            //Debug.ReadKey();
        }

    }
}
