using Creational.Builder.Example2.Builder;
using Creational.Builder.Example2.Cars;

namespace Creational.Builder.Example2.Factory
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
