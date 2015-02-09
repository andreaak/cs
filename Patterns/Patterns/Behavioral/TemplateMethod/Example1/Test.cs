using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Patterns.Behavioral.TemplateMethod.Example1.Food;

namespace Patterns.Behavioral.TemplateMethod.Example1
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
