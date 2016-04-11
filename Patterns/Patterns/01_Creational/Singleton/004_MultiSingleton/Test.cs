using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Patterns.Creational.Singleton._004_MultiSingleton
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Singleton[] singletons = new Singleton[10];

            Singleton.SetLimit = 5;

            for (int i = 0; i < 10; i++)
                singletons[i] = Singleton.Instance();

            for (int i = 0; i < 10; i++)
                Debug.WriteLine(singletons[i].ID);

        }

        [TestMethod]
        public void Test2()
        {
            Singleton2[] singletons = new Singleton2[10];

            for (int i = 0; i < 10; i++)
                singletons[i] = Singleton2.Instance();

            for (int i = 0; i < 10; i++)
                Debug.WriteLine(singletons[i].ID);

        }
    }
}
