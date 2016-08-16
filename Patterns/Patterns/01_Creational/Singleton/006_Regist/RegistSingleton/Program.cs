using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._01_Creational.Singleton._006_Regist.RegistSingleton
{
    [TestFixture]
    public class Test
    {
        [Test]
        [STAThread]
        public void Test1()
        {
            Singleton simple1 = Singleton.Instance();
            Singleton simple2 = Singleton.Instance();
            Check(simple1, simple2);

            SingletonBig big1 = SingletonBig.Instance();
            SingletonBig big2 = SingletonBig.Instance();
            Check(big1, big2);

            SingletonSmall small1 = SingletonSmall.Instance();
            SingletonSmall small2 = SingletonSmall.Instance();
            Check(small1, small2);

            // Delay.
            //Debug.ReadKey();
        }
        static void Check(object o1, object o2)
        {
            Debug.WriteLine(o1.GetType().Name);
            Debug.WriteLine(o1.GetHashCode() + " = " + o2.GetHashCode() + "\n");
        } 
    }
}
