using NUnit.Framework;

namespace Patterns._03_Behavioral.Visitor._001_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
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
