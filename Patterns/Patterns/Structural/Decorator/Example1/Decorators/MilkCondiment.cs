using Patterns.Structural.Decorator.Beverage;

namespace Patterns.Structural.Decorator.Decorators
{
    class MilkCondiment : CondimentsDecoratorBase
    {
        private BeverageBase _beverage;

        public MilkCondiment(BeverageBase beverage)
        {
            _beverage = beverage;
            Desctiption = _beverage.GetDescription() + " +Milk";
        }

        public override double GetCost()
        {
            return _beverage.GetCost() + 50;
        }
    }
}
