using NUnit.Framework;

namespace Patterns._01_Creational.Singleton._003_ThreadSafe
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Singleton singleton1 = Singleton.GetInstance();
            Singleton singleton2 = Singleton.GetInstance();
        }
    }
}
