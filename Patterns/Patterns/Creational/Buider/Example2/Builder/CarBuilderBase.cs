using Creational.Builder.Example2.Cars;
namespace Creational.Builder.Example2.Builder
{
    public abstract class CarBuilderBase
    {
        protected Car car;

        protected CarBuilderBase()
        {
            car = new Car();
        }

        public Car GetCar()
        {
            return car;
        }

        public abstract void BuildMultimedia();
        public abstract void BuildWheels();
        public abstract void BuildEngine();
        public abstract void BuildFrames();
        public abstract void BuildLuxury();
        public abstract void BuildSafety();
    }
}