using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._03_CS7
{

    [TestFixture]
    public class _14_LiteralDefault
    {
#if CS7
        [Test]
        public void TestBinaryLiterals_1()
        {
            int mylnt = default;
            Debug.WriteLine(mylnt);//0
        }
#endif
    }

}
