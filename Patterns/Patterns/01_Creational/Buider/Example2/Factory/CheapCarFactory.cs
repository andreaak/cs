using System;
using Creational.Builder.Example2.Builder;
using Creational.Builder.Example2.Cars;

namespace Creational.Builder.Example2.Factory
{
    public class CheapCarFactory : CarFactoryBase
    {
        public CheapCarFactory(CarBuilderBase builder) : base(builder)
        {
        }

        public override Car Construct()
        {
            carBuilder.BuildFrames();
            carBuilder.BuildEngine();
            carBuilder.BuildWheels();
            carBuilder.BuildSafety();

            return carBuilder.GetCar();
        }
    }
}
