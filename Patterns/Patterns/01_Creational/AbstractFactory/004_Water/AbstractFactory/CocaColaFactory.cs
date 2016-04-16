using Patterns._01_Creational.AbstractFactory._004_Water.AbstractBottle;
using Patterns._01_Creational.AbstractFactory._004_Water.AbstractWater;

namespace Patterns._01_Creational.AbstractFactory._004_Water.AbstractFactory
{
    class CocaColaFactory : AbstractFactory
    {
        public override AbstractWater.AbstractWater CreateWater()
        {
            return new CocaColaWater();
        }

        public override AbstractBottle.AbstractBottle CreateBottle()
        {
            return new CocaColaBottle();
        }
    }
}
