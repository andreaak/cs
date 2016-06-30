namespace CS_TDD._006_NSubstitute.Setup
{
    class Controller
    {
        private ICommand command;
        private IConnection connection;

        public Controller(IConnection connection, ICommand command)
        {
            this.connection = connection;
            this.command = command;
        }

        public void DoStuff()
        {
            connection.Open();
            command.Execute(connection);
            connection.Close();
        }
    }
}
