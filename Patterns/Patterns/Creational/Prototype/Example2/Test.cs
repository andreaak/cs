using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creational.Prototype.Example2
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Prototype prototype = null;
            Prototype original = null;

            prototype = new ConcretePrototype1(1);
            original = prototype.Clone();

            prototype = new ConcretePrototype2(2);
            original = prototype.Clone();
        }

    }
}
