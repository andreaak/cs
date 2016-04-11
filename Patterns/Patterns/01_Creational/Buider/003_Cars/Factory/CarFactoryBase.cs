using Creational.Builder._003_Cars.Builder;
using Creational.Builder._003_Cars.Cars;

namespace Creational.Builder._003_Cars.Factory
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
