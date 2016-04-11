using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Patterns.Creational.Singleton._009_Lazy
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Singleton singleton1 = Singleton.Instance;
            Singleton singleton2 = Singleton.Instance;

            Debug.WriteLine(singleton1.GetHashCode());
            Debug.WriteLine(singleton2.GetHashCode());
        }
    }
}
