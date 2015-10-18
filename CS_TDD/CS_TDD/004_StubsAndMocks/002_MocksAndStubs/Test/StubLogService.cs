using System;

namespace CS_TDD._004_StubsAndMocks._002_MocksAndStubs.Test
{
    public class StubLogService : IWebService
    {
        public Exception ToThrow;
        public void LogError(string message)
        {
            if (ToThrow != null)
            {
                throw ToThrow;
            }
        }
    }
}
