using System;

namespace CS_TDD._004_StubsAndMocks._020_NSubstitute.Setup
{
    public interface ICommand
    {
        void Execute();

        event EventHandler Executed;

        void Execute(IConnection connection);
    }
}
