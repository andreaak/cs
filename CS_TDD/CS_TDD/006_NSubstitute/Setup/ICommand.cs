using System;

namespace CS_TDD._006_NSubstitute.Setup
{
    public interface ICommand
    {
        void Execute();

        event EventHandler Executed;

        void Execute(IConnection connection);
    }
}
