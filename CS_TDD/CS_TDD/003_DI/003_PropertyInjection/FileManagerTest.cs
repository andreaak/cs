using CS_TDD._003_DI._000_Base.Test;
using NUnit.Framework;

namespace CS_TDD._003_DI._003_PropertyInjection
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void PropertyInjectionTest()
        {
            FileManager mgr = new FileManager();
            mgr.DataAccessObject = new StubFileDataObject();
            Assert.IsTrue(mgr.FindLogFile("file2.log"));
        }
    }
}
