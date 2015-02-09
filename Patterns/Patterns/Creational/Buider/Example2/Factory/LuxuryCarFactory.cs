using Creational.Builder.Example2.Builder;
using Creational.Builder.Example2.Cars;

namespace Creational.Builder.Example2.Factory
{
    public class LuxuryCarFactory : CarFactoryBase
    {
        public LuxuryCarFactory(CarBuilderBase builder) : base(builder)
        {
        }

        public override Car Construct()
        {
            carBuilder.BuildFrames();
            carBuilder.BuildEngine();
            carBuilder.BuildWheels();
            carBuilder.BuildSafety();
            carBuilder.BuildMultimedia();
            carBuilder.BuildLuxury();

            return carBuilder.GetCar();
        }
    }
}
