using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._03_Behavioral.TemplateMethod._001_Food.Food;

namespace Patterns._03_Behavioral.TemplateMethod._001_Food
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
