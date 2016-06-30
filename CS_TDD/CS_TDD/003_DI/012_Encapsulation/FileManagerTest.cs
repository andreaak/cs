using CS_TDD._003_DI._000_Base.Test;
using NUnit.Framework;

namespace CS_TDD._003_DI._012_Encapsulation
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void EncapsulationTest()
        {
#if DEBUG
            FileManager mgr = new FileManager(new StubFileDataObject());
            Assert.IsTrue(mgr.FindLogFile("file2.log"));
#endif
        }
    }
}
