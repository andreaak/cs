namespace Patterns._01_Creational.AbstractFactory._004_Water.AbstractFactory
{
    abstract class AbstractFactory
    {
        public abstract AbstractWater.AbstractWater CreateWater();
        public abstract AbstractBottle.AbstractBottle CreateBottle();
    }
}
