using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace Creational.FactoryMethod.Example2
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Creator creator = null;
            Product product = null;

            creator = new ConcreteCreator();
            product = creator.FactoryMethod();

            creator.AnOperation();
        }

    }
}
