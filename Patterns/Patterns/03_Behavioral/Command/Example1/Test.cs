using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Patterns.Behavioral.Command.Example1.Commands;
using Patterns.Behavioral.Command.Example1.ControlledSystems;
using System.Collections.Generic;

namespace Patterns.Behavioral.Command.Example1
{
    [TestClass]
    public class Test
    {

        
        [TestMethod]
        public void Test1()
        {
            var remote = new RemoteControl();
            string userInput;

            remote.SetCommandForButton(1, new LightsCommand(new Light()));
            remote.SetCommandForButton(2, new TvCommand(new Tv()));
            remote.SetCommandForButton(3, new MusicCommand(new Music()));
            remote.SetCommandForButton(4, new TeapotCommand(new Teapot()));

            var teapotCommand = new TeapotCommand(new Teapot());
            var tvCommand = new TvCommand(new Tv());
            var macroCommand = new MacroCommand(new List<ICommand> {teapotCommand, tvCommand});
            remote.SetCommandForButton(5, macroCommand);

            do
            {
                Console.WriteLine("Выберите вариант ниже:");
                Console.WriteLine(remote);

                Debug.Write("\nВаш выбор: ");
                var input = "2";// Console.ReadLine();
                int buttonId;
                int.TryParse(input, out buttonId);

                remote.PushButton(buttonId);
                remote.UndoButton(buttonId);

                Debug.Write("\nВы хотите продолжить (y/n): ");
                userInput = "n";//Console.ReadLine();
            } while (userInput == "y");
        }
    }
}
