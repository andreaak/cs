using CS_TDD._004_StubsAndMocks._004_Mocks;
using NUnit.Framework;
using Rhino.Mocks;

namespace CS_TDD._004_StubsAndMocks._019_RhynoMocks._006_RhynoMocks.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public static void RhinoMocksTest1()
        {
            MockRepository rhinoEngine = new MockRepository();

            // Создание динамического Mock-объекта
            ILogService logService = rhinoEngine.DynamicMock<ILogService>();
            
            // Создание сценария
            using (rhinoEngine.Record())
            {
                logService.LogError("TestErrorMessage");
            }

            logService.LogError("TestErrorMessage");

            // Проверка сценария
            rhinoEngine.Verify(logService);
        }

        [Test]
        public static void RhinoMocksTest2()
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
