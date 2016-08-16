using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._01_Creational.Singleton._005_Inheritance
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            DerivedSingleton instance1 = DerivedSingleton.Instance();
            DerivedSingleton instance2 = DerivedSingleton.Instance();
            Debug.WriteLine(ReferenceEquals(instance1, instance2));
        }
    }
}
