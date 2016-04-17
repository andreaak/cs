using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.TemplateMethod._003_TemplateMethod
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            var flag = new UkraineFlag();
            flag.Draw();

            // Delay.
           // Console.ReadKey();
        }
    }
}
