using Patterns.Structural.Decorator.Beverage;

namespace Patterns.Structural.Decorator.Decorators
{
    class ChocolateCondiment : CondimentsDecoratorBase
    {
        private BeverageBase _beverage;

        public ChocolateCondiment(BeverageBase beverage)
        {
            _beverage = beverage;
            Desctiption = _beverage.GetDescription() + " +Chocolate";
        }

        public override double GetCost()
        {
            return _beverage.GetCost() + 70;
        }
    }
}
