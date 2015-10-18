using NUnit.Framework;
using Rhino.Mocks;
using CS_TDD._004_StubsAndMocks._004_Mocks;

namespace CS_TDD._004_StubsAndMocks._007_RhynoMocks.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public static void RhinoMocksTest_2()
        {
            string fileName = "TestFileName.exe";

            MockRepository rhinoEngine = new MockRepository();
            // Создание динамического Mock-объекта
            ILogService logService = rhinoEngine.DynamicMock<ILogService>();
            // Создание сценария
            using (rhinoEngine.Record())
            {
                logService.LogError("FileExtension error: " + fileName);
            }
            // Создание запуск отработки кода
            FileManager fileManager = new FileManager(logService);
            fileManager.Analize(fileName);
            // Проверка сценария
            rhinoEngine.Verify(logService);
        }
    }
}
