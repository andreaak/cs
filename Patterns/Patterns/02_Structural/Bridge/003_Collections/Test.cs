using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Patterns.Structural.Bridge._003_Collections
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Abstraction abstraction;

            abstraction = new RefinedAbstraction(3);
            Debug.WriteLine(abstraction.Implementor.GetType());

            abstraction = new RefinedAbstraction(30);
            Debug.WriteLine(abstraction.Implementor.GetType());
        }
    }
}
