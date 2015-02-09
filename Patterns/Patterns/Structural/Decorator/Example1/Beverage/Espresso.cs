namespace Patterns.Structural.Decorator.Beverage
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
