using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Visitor._003_Visitor
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            ObjectStructure structure = new ObjectStructure();

            structure.Add(new ElementA());
            structure.Add(new ElementB());

            structure.Accept(new ConcreteVisitor());
        }
    }
}
