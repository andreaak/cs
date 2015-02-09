namespace Patterns.Structural.Decorator.Beverage
{
    public abstract class BeverageBase
    {
        protected string Desctiption = "";

        public string GetDescription()
        {
            return Desctiption;
        }

        public abstract double GetCost();
    }
}
