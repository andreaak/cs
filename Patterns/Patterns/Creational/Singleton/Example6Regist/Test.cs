using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using Sing1 = Patterns.Creational.Singleton.Example6.RegistSingleton;
using Sing2 = Patterns.Creational.Singleton.Example6.RegistSingletonGen;

namespace Patterns.Creational.Singleton.Example6
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Sing1.Singleton simple1 = Sing1.Singleton.Instance();
            Sing1.Singleton simple2 = Sing1.Singleton.Instance();
            Check(simple1, simple2);

            Sing1.SingletonBig big1 = Sing1.SingletonBig.Instance();
            Sing1.SingletonBig big2 = Sing1.SingletonBig.Instance();
            Check(big1, big2);

            Sing1.SingletonSmall small1 = Sing1.SingletonSmall.Instance();
            Sing1.SingletonSmall small2 = Sing1.SingletonSmall.Instance();
            Check(small1, small2);
        }

        [TestMethod]
        public void Test2()
        {
            Sing2.Singleton simple1 = Sing2.Singleton.Instance();
            Sing2.Singleton simple2 = Sing2.Singleton.Instance();
            Check(simple1, simple2);

            Sing2.SingletonBig big1 = Sing2.SingletonBig.Instance();
            Sing2.SingletonBig big2 = Sing2.SingletonBig.Instance();
            Check(big1, big2);

            Sing2.SingletonSmall small1 = Sing2.SingletonSmall.Instance();
            Sing2.SingletonSmall small2 = Sing2.SingletonSmall.Instance();
            Check(small1, small2);
        }

        static void Check(object o1, object o2)
        {
            Debug.WriteLine(o1.GetType().Name);
            Debug.WriteLine(o1.GetHashCode() + " = " + o2.GetHashCode() + "\n");
        }
    }
}
