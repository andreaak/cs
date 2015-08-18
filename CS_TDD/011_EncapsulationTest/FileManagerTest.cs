using CS_TDD._003_DI._000_Base.Test;
using CS_TDD._003_DI._011_Encapsulation;
using NUnit.Framework;

namespace _011_EncapsulationTest
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FindLogFileTest2()
        {
            FileManager mgr = new FileManager(new StubFileDataObject());
            Assert.IsTrue(mgr.FindLogFile("file2.log"));
        }
    }
}
