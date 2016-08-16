using NUnit.Framework;
using Patterns._01_Creational.FactoryMethod._006_FactoryMethodWithArgument.Creators;
using Patterns._01_Creational.FactoryMethod._006_FactoryMethodWithArgument.Products;

namespace Patterns._01_Creational.FactoryMethod._006_FactoryMethodWithArgument
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Creator creator = new MyCreator();
            Product product = creator.Create(ProductType.THEIRS);
        }

    }
}
