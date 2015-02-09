using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Patterns.Creational.Singleton.Example5
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            DerivedSingleton instance1 = DerivedSingleton.Instance();
            DerivedSingleton instance2 = DerivedSingleton.Instance();
            Console.WriteLine(ReferenceEquals(instance1, instance2));
        }
    }
}
