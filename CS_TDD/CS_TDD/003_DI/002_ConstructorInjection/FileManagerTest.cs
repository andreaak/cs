using CS_TDD._003_DI._000_Base.Test;
using NUnit.Framework;

namespace CS_TDD._003_DI._002_ConstructorInjection
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void ConstructorInjectionTest()
        {
            FileManager mgr = new FileManager(new StubFileDataObject());
            Assert.IsTrue(mgr.FindLogFile("file2.log"));
        }
    }
}
