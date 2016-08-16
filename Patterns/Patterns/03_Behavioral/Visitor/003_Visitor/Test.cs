using NUnit.Framework;

namespace Patterns._03_Behavioral.Visitor._003_Visitor
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            ObjectStructure structure = new ObjectStructure();

            structure.Add(new ElementA());
            structure.Add(new ElementB());

            structure.Accept(new ConcreteVisitor());
        }
    }
}
