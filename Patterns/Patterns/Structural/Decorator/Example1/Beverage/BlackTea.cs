namespace Patterns.Structural.Decorator.Beverage
{
    class BlackTea : BeverageBase
    {
        public BlackTea()
        {
            Desctiption = "Black tea from teabag";
        }

        public override double GetCost()
        {
            return 25;
        }
    }
}
