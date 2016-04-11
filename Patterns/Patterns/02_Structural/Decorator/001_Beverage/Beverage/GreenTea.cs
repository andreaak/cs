namespace Patterns.Structural.Decorator._001_Beverage.Beverage
{
    class GreenTea : BeverageBase
    {
        public GreenTea()
        {
            Desctiption = "Green leaf tea";
        }

        public override double GetCost()
        {
            return 125;
        }
    }
}
