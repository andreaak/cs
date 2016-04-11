using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory._002_Base
{
    class ProductB1 : AbstractProductB
    {
        public override void Interact(AbstractProductA apa)
        {
            Debug.WriteLine(this + " interacts with " + apa);
        }
    }
}
