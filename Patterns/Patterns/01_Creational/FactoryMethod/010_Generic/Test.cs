using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._01_Creational.FactoryMethod._010_Generic.Creators;
using Patterns._01_Creational.FactoryMethod._010_Generic.Products;
using Patterns._01_Creational.FactoryMethod._010_Generic.Products.IProduct;

namespace Patterns._01_Creational.FactoryMethod._010_Generic
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
