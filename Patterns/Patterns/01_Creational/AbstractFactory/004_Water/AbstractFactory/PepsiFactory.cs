using Patterns._01_Creational.AbstractFactory._004_Water.AbstractBottle;
using Patterns._01_Creational.AbstractFactory._004_Water.AbstractWater;

namespace Patterns._01_Creational.AbstractFactory._004_Water.AbstractFactory
{
    class PepsiFactory : AbstractFactory
    {
        public override AbstractWater.AbstractWater CreateWater()
        {
            return new PepsiWater();
        }

        public override AbstractBottle.AbstractBottle CreateBottle()
        {
            return new PepsiBottle();
        }
    }
}
