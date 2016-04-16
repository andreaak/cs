using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._01_Creational.Singleton._005_Inheritance
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            DerivedSingleton instance1 = DerivedSingleton.Instance();
            DerivedSingleton instance2 = DerivedSingleton.Instance();
            Debug.WriteLine(ReferenceEquals(instance1, instance2));
        }
    }
}
