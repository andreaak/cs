﻿using System.Collections.Generic;
using System.Text;

namespace Patterns._03_Behavioral.Command._001_RemoteControl.Commands
{
    public class MacroCommand : ICommand
    {
        private List<ICommand> _innerCommands; 

        public MacroCommand(List<ICommand> commands)
        {
            _innerCommands = commands;
        }

        public void Execute()
        {
            foreach (var innerCommand in _innerCommands)
            {
                innerCommand.Execute();
            }
        }

        public void Undo()
        {
            foreach (var innerCommand in _innerCommands)
            {
                innerCommand.Undo();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var innerCommand in _innerCommands)
                sb.AppendFormat("{0}, ", innerCommand);

            return sb.ToString();
        }
    }
}
