using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._01_Creational.FactoryMethod._002_Base.Creator;

namespace Patterns._01_Creational.FactoryMethod._002_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Creator.Creator creator = null;
            Product.Product product = null;

            creator = new ConcreteCreator();
            product = creator.FactoryMethod();

            creator.AnOperation();
        }

    }
}
