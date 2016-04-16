using Patterns._01_Creational.Buider._004_House.House;

namespace Patterns._01_Creational.Buider._004_House.Builder
{
    class ConcreteBuilder : Builder
    {
        House.House house = new House.House();

        public override void BuildBasement()
        {
            house.Add(new Basement());
        }

        public override void BuildStorey()
        {
            house.Add(new Storey());
        }

        public override void BuildRoof()
        {
            house.Add(new Roof());
        }

        public override House.House GetResult()
        {
            return house;
        }
    }
}
