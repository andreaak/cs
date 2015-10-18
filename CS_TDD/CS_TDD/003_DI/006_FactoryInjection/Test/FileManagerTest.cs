using CS_TDD._003_DI._000_Base.Test;
using NUnit.Framework;

namespace CS_TDD._003_DI._006_FactoryInjection.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FactoryInjectionTest()
        {
            FactoryClass.SetDataAccessObject(new StubFileDataObject());

            FileManager mgr = new FileManager();

            Assert.IsTrue(mgr.FindLogFile("file1.txt"));
        }
    }
}
