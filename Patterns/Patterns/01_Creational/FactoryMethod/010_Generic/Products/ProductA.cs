using System.Diagnostics;

namespace Patterns._01_Creational.FactoryMethod._010_Generic.Products
{
    class ProductA : IProduct.IProduct
    {
        public ProductA()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
