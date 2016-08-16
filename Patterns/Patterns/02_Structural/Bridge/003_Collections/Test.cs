using System.Diagnostics;
using NUnit.Framework;
using Patterns._02_Structural.Bridge._003_Collections.Abstraction;

namespace Patterns._02_Structural.Bridge._003_Collections
{
    [TestFixture]
    public class Test
    {
        [Test]
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
