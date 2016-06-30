using CS_TDD._003_DI._000_Base.Test;
using NUnit.Framework;

namespace CS_TDD._003_DI._004_InterfaceInjection
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void InterfaceInjectionTest()
        {
            FileManager mgr = new FileManager();
            Assert.IsTrue(mgr.FindLogFile("file2.log", new StubFileDataObject()));
        }
    }
}
