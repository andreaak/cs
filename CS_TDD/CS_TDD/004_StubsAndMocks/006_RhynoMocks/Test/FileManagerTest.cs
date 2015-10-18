using CS_TDD._004_StubsAndMocks._004_Mocks;
using NUnit.Framework;
using Rhino.Mocks;

namespace CS_TDD._004_StubsAndMocks._006_RhynoMocks.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public static void RhinoMocksTest_1()
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
    }
}
