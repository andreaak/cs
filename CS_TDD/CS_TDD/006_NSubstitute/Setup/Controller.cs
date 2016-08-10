using System.Collections.Generic;

namespace CS_TDD._006_NSubstitute.Setup
{
    public class Controller
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

        public Controller(string[] str)
        {
        }

        public Controller(int a, string str)
        {
        }
    }
}
