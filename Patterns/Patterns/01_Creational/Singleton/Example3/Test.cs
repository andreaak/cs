using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Patterns.Creational.Singleton.Example3
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Singleton instance1 = Singleton.Instance();
            Singleton instance2 = Singleton.Instance();
            Console.WriteLine(ReferenceEquals(instance1, instance2));

            instance1.SingletonOperation();
            string singletonData = instance1.GetSingletonData();
            Console.WriteLine(singletonData);
        }
    }
}
