namespace Patterns._03_Behavioral.Strategy._002_Base
{
    class Context
    {
        Strategy.Strategy strategy;

        public Context(Strategy.Strategy strategy)
        {
            this.strategy = strategy;
        }

        public void ContextInterface()
        {
            strategy.AlgorithmInterface();
        }
    }
}
