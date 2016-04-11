using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns.Creational.Singleton._003_ThreadSafe
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Singleton singleton1 = Singleton.GetInstance();
            Singleton singleton2 = Singleton.GetInstance();
        }
    }
}
