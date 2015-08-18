using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._000_Base.Test;
using NUnit.Framework;

namespace CS_TDD._003_DI._005_LocalFactoryMethod
{
    class FileManagerUnderTest : FileManager
    {
        protected override IDataAccessObject LocalFactoryMethod()
        {
            return new TestDataObject();
        }
    }


    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void LocalFactoryTest()
        {
            var mgr = new FileManagerUnderTest();

            var result = mgr.FindLogFile("file1.txt");

            Assert.IsTrue(result);
        }
    }
}
