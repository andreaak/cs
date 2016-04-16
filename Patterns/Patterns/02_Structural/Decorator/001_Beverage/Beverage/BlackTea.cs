namespace Patterns._02_Structural.Decorator._001_Beverage.Beverage
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
