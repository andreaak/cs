using System.Diagnostics;

namespace Patterns._01_Creational.FactoryMethod._010_Generic.Products
{
    class ProductB : IProduct.IProduct
    {
        public ProductB()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
