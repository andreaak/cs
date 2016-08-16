using System.Diagnostics;
using NUnit.Framework;
using Patterns._03_Behavioral.TemplateMethod._001_Food.Food;

namespace Patterns._03_Behavioral.TemplateMethod._001_Food
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            var hotDog = new HotDog();
            var hamburger = new Hamburger();

            Debug.WriteLine("\nHotDog:");
            hotDog.Prepare();
            Debug.WriteLine("\nHamburger:");
            hamburger.Prepare();
        }
    }
}
