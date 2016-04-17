using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._04_Class._15_ExtensionMethods
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestExtensionMethods1()
        {
            SealedClass obj = null;
            obj.Extension();
        }
    }
}
