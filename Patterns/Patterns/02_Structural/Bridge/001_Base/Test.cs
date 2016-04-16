using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._02_Structural.Bridge._001_Base.Abstraction;
using Patterns._02_Structural.Bridge._001_Base.Implementor;

namespace Patterns._02_Structural.Bridge._001_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Implementor.Implementor implementor;
            Abstraction.Abstraction abstraction;

            implementor = new ConcreteImplementorA();
            abstraction = new RefinedAbstraction(implementor);
            abstraction.Operation();

            implementor = new ConcreteImplementorB();
            abstraction = new RefinedAbstraction(implementor);
            abstraction.Operation();
        }
    }
}
