using System;
using NUnit.Framework;

//can use only static classes

namespace CSTest._25_CS_NewFeatures._03_CS6
{
    [TestFixture]
    public class _09_Other
    {
#if CS6
        [Test]
        public void Test1()
        {
            Console.WriteLine("Test");
        }
#endif
    }
}
