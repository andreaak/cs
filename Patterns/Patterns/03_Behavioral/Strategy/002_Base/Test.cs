using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._03_Behavioral.Strategy._002_Base.Strategy;

namespace Patterns._03_Behavioral.Strategy._002_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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
