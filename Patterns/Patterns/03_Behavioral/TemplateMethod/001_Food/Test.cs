using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Behavioral.TemplateMethod._001_Food.Food;
using System.Diagnostics;

namespace Patterns.Behavioral.TemplateMethod._001_Food
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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
