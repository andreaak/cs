using NUnit.Framework;

namespace CS_TDD._003_DI._001_Problem
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void ProblemTest()
        {
            FileManager mgr = new FileManager();
            Assert.IsTrue(mgr.FindLogFile("file2.log"));
        }
    }
}
