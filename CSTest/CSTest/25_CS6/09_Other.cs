using NUnit.Framework;
using static System.Console;//can use only static classes

namespace CSTest._25_CS6.Setup
{
    [TestFixture]
    public class _09_Other
    {
#if CS6
        [Test]
        public void Test1()
        {
            WriteLine("Test");
        }
#endif
    }
}
