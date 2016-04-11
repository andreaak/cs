namespace Patterns.Structural.Decorator._001_Beverage.Beverage
{
    class Espresso : BeverageBase
    {
        public Espresso()
        {
            Desctiption = "Small portion of strong coffee";
        }

        public override double GetCost()
        {
            return 150;
        }
    }
}
