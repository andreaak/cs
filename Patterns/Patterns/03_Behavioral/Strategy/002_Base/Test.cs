using NUnit.Framework;
using Patterns._03_Behavioral.Strategy._002_Base.Strategy;

namespace Patterns._03_Behavioral.Strategy._002_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Context context;
            context = new Context(new ConcreteStrategyA());
            context.ContextInterface();
            context = new Context(new ConcreteStrategyB());
            context.ContextInterface();
            context = new Context(new ConcreteStrategyC());
            context.ContextInterface();
        }
    }
}
