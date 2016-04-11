using System.Diagnostics;

namespace Creational.FactoryMethod._006_FactoryMethodWithArgument
{
    abstract class Product
    {
        public Product()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
