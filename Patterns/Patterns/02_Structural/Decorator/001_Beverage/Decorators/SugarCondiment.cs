using Patterns.Structural.Decorator._001_Beverage.Beverage;

namespace Patterns.Structural.Decorator._001_Beverage.Decorators
{
    class SugarCondiment : CondimentsDecoratorBase
    {
        private BeverageBase _beverage;

        public SugarCondiment(BeverageBase beverage)
        {
            _beverage = beverage;
            Desctiption = _beverage.GetDescription() + " +Sugar";
        }

        public override double GetCost()
        {
            return _beverage.GetCost() + 10;
        }
    }
}
