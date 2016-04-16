using System.Diagnostics;
using Patterns._01_Creational.AbstractFactory._006_BaseModified.AbstractProductA;

namespace Patterns._01_Creational.AbstractFactory._006_BaseModified.AbstractProductB
{
    class ProductB2 : IAbstractProductB
    {
        public void Interact(IAbstractProductA a)
        {
            Debug.WriteLine(this.GetType().Name + " взаимодействует с " + a.GetType().Name);
        }
    }
}
