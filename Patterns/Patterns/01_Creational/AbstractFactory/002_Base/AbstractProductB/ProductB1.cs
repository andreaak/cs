using System.Diagnostics;

namespace Patterns._01_Creational.AbstractFactory._002_Base.AbstractProductB
{
    class ProductB1 : AbstractProductB
    {
        public override void Interact(AbstractProductA.AbstractProductA apa)
        {
            Debug.WriteLine(this + " interacts with " + apa);
        }
    }
}
