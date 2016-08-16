using NUnit.Framework;
using Patterns._01_Creational.FactoryMethod._002_Base.Creator;

namespace Patterns._01_Creational.FactoryMethod._002_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
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
