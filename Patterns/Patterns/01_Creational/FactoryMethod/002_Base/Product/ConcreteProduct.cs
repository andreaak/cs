using System.Diagnostics;

namespace Patterns._01_Creational.FactoryMethod._002_Base.Product
{
    class ConcreteProduct : Product
    {
        public ConcreteProduct()
        {
            Debug.WriteLine(this.GetHashCode());
        }
    }
}
