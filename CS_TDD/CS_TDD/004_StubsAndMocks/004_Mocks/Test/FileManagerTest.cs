using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._004_Mocks.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public static void MockTest_1()
        {
            MockLogService mockService = new MockLogService();

            FileManager mgr = new FileManager(mockService);

            mgr.Analize("SomeFile.log");

            Assert.AreEqual("FileExtension error: SomeFile.log", mockService.lastError);
        }
    }
}
