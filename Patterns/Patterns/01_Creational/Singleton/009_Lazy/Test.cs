using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._01_Creational.Singleton._009_Lazy
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Singleton singleton1 = Singleton.Instance;
            Singleton singleton2 = Singleton.Instance;

            Debug.WriteLine(singleton1.GetHashCode());
            Debug.WriteLine(singleton2.GetHashCode());
        }
    }
}
