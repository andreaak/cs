using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns.Structural.Bridge.Example1
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Implementor implementor;
            Abstraction abstraction;

            implementor = new ConcreteImplementorA();
            abstraction = new RefinedAbstraction(implementor);
            abstraction.Operation();

            implementor = new ConcreteImplementorB();
            abstraction = new RefinedAbstraction(implementor);
            abstraction.Operation();
        }
    }
}
