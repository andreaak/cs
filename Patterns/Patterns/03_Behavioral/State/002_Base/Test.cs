using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.State._002_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Context context = new Context(new ConcreteStateA());
            context.Request();
            context.Request();
        }
    }
}
