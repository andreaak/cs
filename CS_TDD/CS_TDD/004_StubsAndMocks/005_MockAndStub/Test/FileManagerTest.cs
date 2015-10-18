using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._005_MockAndStub.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public static void MockAndStubTest()
        {
            var logService = new StubLogService();
            var mailService = new MockMailService();

            FileManager mgr = new FileManager(logService, mailService);

            string fileName = "SomeFile.log";

            mgr.Analize(fileName);

            Assert.AreEqual("FileExtension error: " + fileName, mailService.message);
        }
    }
}
