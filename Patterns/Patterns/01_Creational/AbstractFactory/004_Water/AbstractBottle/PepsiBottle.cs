using System.Diagnostics;

namespace Patterns._01_Creational.AbstractFactory._004_Water.AbstractBottle
{
    class PepsiBottle : AbstractBottle
    {
        public override void Interact(AbstractWater.AbstractWater water)
        {
            Debug.WriteLine(this + " interacts with " + water);
        }
    }
}
