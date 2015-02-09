
using System.Diagnostics;
namespace Creational.FactoryMethod.Example6
{
    abstract class Product
    {
        public Product()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
