using System.Diagnostics;
using NUnit.Framework;
using Patterns._01_Creational.Singleton._006_Regist.RegistSingleton;

namespace Patterns._01_Creational.Singleton._006_Regist
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            RegistSingleton.Singleton simple1 = RegistSingleton.Singleton.Instance();
            RegistSingleton.Singleton simple2 = RegistSingleton.Singleton.Instance();
            Check(simple1, simple2);

            SingletonBig big1 = SingletonBig.Instance();
            SingletonBig big2 = SingletonBig.Instance();
            Check(big1, big2);

            SingletonSmall small1 = SingletonSmall.Instance();
            SingletonSmall small2 = SingletonSmall.Instance();
            Check(small1, small2);
        }

        [Test]
        public void Test2()
        {
            RegistSingletonGen.Singleton simple1 = RegistSingletonGen.Singleton.Instance();
            RegistSingletonGen.Singleton simple2 = RegistSingletonGen.Singleton.Instance();
            Check(simple1, simple2);

            RegistSingletonGen.SingletonBig big1 = RegistSingletonGen.SingletonBig.Instance();
            RegistSingletonGen.SingletonBig big2 = RegistSingletonGen.SingletonBig.Instance();
            Check(big1, big2);

            RegistSingletonGen.SingletonSmall small1 = RegistSingletonGen.SingletonSmall.Instance();
            RegistSingletonGen.SingletonSmall small2 = RegistSingletonGen.SingletonSmall.Instance();
            Check(small1, small2);
        }

        static void Check(object o1, object o2)
        {
            Debug.WriteLine(o1.GetType().Name);
            Debug.WriteLine(o1.GetHashCode() + " = " + o2.GetHashCode() + "\n");
        }
    }
}
