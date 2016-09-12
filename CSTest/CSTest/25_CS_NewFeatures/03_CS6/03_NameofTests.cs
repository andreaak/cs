using NUnit.Framework;
using System;

namespace CSTest._25_CS6
{
    [TestFixture]
    public class _03_NameofTests
    {
#if CS6
        [Test]
        public void Test1()
        {
            Assert.Catch<ArgumentNullException>(() => TestMethod(null));
        }

        private void TestMethod(string arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
        }
#endif
    }
}
