using System;

namespace CS_TDD._006_NSubstitute.Setup
{
    public class CommandRunner
    {
        ICommand command;
        public CommandRunner(ICommand command)
        {
            this.command = command;
        }

        public void DoSomething()
        {
            command.Execute();
        }

        public void DontDoAnything()
        {
        }
    }

    public class CommandWatcher
    {
        ICommand command;

        public CommandWatcher(ICommand command)
        {
            command.Executed += OnExecuted;
        }

        public bool DidStuff { get; private set; }

        public void OnExecuted(object o, EventArgs e)
        {
            DidStuff = true;
        }
    }
}
