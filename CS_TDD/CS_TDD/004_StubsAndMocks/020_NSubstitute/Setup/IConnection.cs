using System;

namespace CS_TDD._004_StubsAndMocks._020_NSubstitute.Setup
{
    public interface IConnection
    {
        event Action SomethingHappened;

        void Open();
        void Close();
    }
}
