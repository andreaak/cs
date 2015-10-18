namespace CS_TDD._004_StubsAndMocks._004_Mocks.Test
{
    class MockLogService : ILogService
    {
        public string lastError;

        public void LogError(string error)
        {
            lastError = error;
        }
    }
}
