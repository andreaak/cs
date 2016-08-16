using NUnit.Framework;

namespace Patterns._03_Behavioral.TemplateMethod._003_TemplateMethod
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            var flag = new UkraineFlag();
            flag.Draw();

            // Delay.
           // Console.ReadKey();
        }
    }
}
