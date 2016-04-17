using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Visitor._001_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            ObjectStructure structure = new ObjectStructure();

            structure.Add(new ConcreteElementA());
            structure.Add(new ConcreteElementB());

            structure.Accept(new ConcreteVisitor1());
            structure.Accept(new ConcreteVisitor2());
        }
    }
}
