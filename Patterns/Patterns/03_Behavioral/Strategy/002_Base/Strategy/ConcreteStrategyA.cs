using System.Diagnostics;

namespace Patterns._03_Behavioral.Strategy._002_Base.Strategy
{
    class ConcreteStrategyA : Strategy
    {
        public override void AlgorithmInterface()
        {
            Debug.WriteLine("ConcreteStrategyA");
        }
    }
}
