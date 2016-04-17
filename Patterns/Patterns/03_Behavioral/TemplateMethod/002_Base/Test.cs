using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.TemplateMethod._002_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            AbstractClass instance = new ConcreteClass();
            instance.TemplateMethod();
        }
    }
}
