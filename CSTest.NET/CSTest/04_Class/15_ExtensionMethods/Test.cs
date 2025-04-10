using NUnit.Framework;

namespace CSTest._04_Class._15_ExtensionMethods
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestExtensionMethods1()
        {
            SealedClass obj = null;
            obj.Extension();
        }
    }
}
