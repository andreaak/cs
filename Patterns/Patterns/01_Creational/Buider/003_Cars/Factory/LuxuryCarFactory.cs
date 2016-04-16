using Patterns._01_Creational.Buider._003_Cars.Builder;
using Patterns._01_Creational.Buider._003_Cars.Cars;

namespace Patterns._01_Creational.Buider._003_Cars.Factory
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
