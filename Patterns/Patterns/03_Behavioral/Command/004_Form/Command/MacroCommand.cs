using System.Collections.Generic;

namespace Patterns._03_Behavioral.Command._004_Form.Command
{
    class MacroCommand : Command
    {
          public readonly List<Command> Commands = new List<Command>();

        public MacroCommand()
        { }
        public MacroCommand(params Command[] commands)
        {
            Commands.AddRange(commands);
        }
      
        public override void Execute()
        {
            foreach (var c in Commands)
                c.Execute();
        }
    }
}
