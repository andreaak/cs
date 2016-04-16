using Patterns._01_Creational.Buider._003_Cars.Builder;
using Patterns._01_Creational.Buider._003_Cars.Cars;

namespace Patterns._01_Creational.Buider._003_Cars.Factory
{
    public abstract class CarFactoryBase
    {
        protected readonly CarBuilderBase carBuilder;

        protected CarFactoryBase(CarBuilderBase builder)
        {
            carBuilder = builder;
        }

        public abstract Car Construct();
    }
}
