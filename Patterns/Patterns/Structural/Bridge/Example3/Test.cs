using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Patterns.Structural.Bridge.Example3
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Abstraction abstraction;

            abstraction = new RefinedAbstraction(3);
            Console.WriteLine(abstraction.Implementor.GetType());

            abstraction = new RefinedAbstraction(30);
            Console.WriteLine(abstraction.Implementor.GetType());
        }
    }
}
