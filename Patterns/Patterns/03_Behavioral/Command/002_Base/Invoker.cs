namespace Patterns._03_Behavioral.Command._002_Base
{
    class Invoker
    {
        Command command;

        public void StoreCommand(Command command)
        {
            this.command = command;
        }

        public void ExecuteCommand()
        {
            command.Execute();
        }
    }
}
