using System.Diagnostics;

namespace Patterns._01_Creational.FactoryMethod._006_FactoryMethodWithArgument.Products
{
    abstract class Product
    {
        public Product()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
