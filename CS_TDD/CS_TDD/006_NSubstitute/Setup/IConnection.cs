using System;

namespace CS_TDD._006_NSubstitute.Setup
{
    public interface IConnection
    {
        event Action SomethingHappened;

        void Open();
        void Close();
    }
}
