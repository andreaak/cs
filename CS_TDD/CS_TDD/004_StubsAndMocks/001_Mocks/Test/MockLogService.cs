namespace CS_TDD._004_StubsAndMocks._001_Mocks.Test
{
    public class MockLogService : IWebService
    {
        public string LastError;

        public void LogError(string message)
        {
            LastError = message;
        }
    }
}
