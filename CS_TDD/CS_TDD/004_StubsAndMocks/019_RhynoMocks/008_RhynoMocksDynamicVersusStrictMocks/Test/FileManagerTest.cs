using NUnit.Framework;
using Rhino.Mocks;

namespace CS_TDD._004_StubsAndMocks._008_RhynoMocksDynamicVersusStrictMocks.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public static void RhinoMocksStrictMockTest()
        {
            string fileName = "TestFileName.exe";

            MockRepository rhinoEngine = new MockRepository();

            ILogService logService = rhinoEngine.StrictMock<ILogService>();

            using (rhinoEngine.Record())
            {
                //logService.OtherMethod();
                logService.LogError("FileExtension error: " + fileName);
            }

            FileManager fileManager = new FileManager(logService);
            fileManager.Analize(fileName);

            rhinoEngine.Verify(logService);
        }

        [Test]
        public static void RhinoMocksDynamicMockTest()
        {
            string fileName = "TestFileName.exe";

            MockRepository rhinoEngine = new MockRepository();

            ILogService logService = rhinoEngine.DynamicMock<ILogService>();

            using (rhinoEngine.Record())
            {
                logService.LogError("FileExtension error: " + fileName);
            }

            FileManager fileManager = new FileManager(logService);
            fileManager.Analize(fileName);

            rhinoEngine.Verify(logService);
        }
    }
}
