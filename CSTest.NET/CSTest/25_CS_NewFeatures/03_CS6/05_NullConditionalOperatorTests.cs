using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace CSTest._25_CS_NewFeatures._03_CS6
{
#if CS6
    [TestFixture]
    public class _05_NullConditionalOperatorTests : IDisposable
    {

        // Elvis operator
        [Test]
        public void Test1()
        {
            ClassicAssert.IsNull(TestMethod(null));
        }

        private string TestMethod(Action action)
        {
            return action?.Method?.Name;
        }

        public void Dispose()
        {
            LogWriter?.Dispose();
        }

        private StreamWriter LogWriter { get; } = new StreamWriter(new MemoryStream());


    }
#endif
}
