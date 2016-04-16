using System.Diagnostics;

namespace Patterns._01_Creational.FactoryMethod._010_Generic.Products
{
    class ProductC : IProduct.IProduct
    {
        public ProductC()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
