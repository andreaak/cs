using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._02_Structural.Bridge._003_Collections.Abstraction;

namespace Patterns._02_Structural.Bridge._003_Collections
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Abstraction.Abstraction abstraction;

            abstraction = new RefinedAbstraction(3);
            Debug.WriteLine(abstraction.Implementor.GetType());

            abstraction = new RefinedAbstraction(30);
            Debug.WriteLine(abstraction.Implementor.GetType());
        }
    }
}
