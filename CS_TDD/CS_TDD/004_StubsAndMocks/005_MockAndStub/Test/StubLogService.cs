using System;
using CS_TDD._004_StubsAndMocks._004_Mocks;

namespace CS_TDD._004_StubsAndMocks._005_MockAndStub.Test
{
    class StubLogService : ILogService
    {
        public void LogError(string error)
        {
            throw new Exception(error);
        }
    }
}
