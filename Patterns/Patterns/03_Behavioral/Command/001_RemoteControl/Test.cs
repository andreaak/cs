using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using Patterns._03_Behavioral.Command._001_RemoteControl.Commands;
using Patterns._03_Behavioral.Command._001_RemoteControl.ControlledSystems;

namespace Patterns._03_Behavioral.Command._001_RemoteControl
{
    [TestFixture]
    public class Test
    {
        [Test]
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
                Debug.WriteLine("Выберите вариант ниже:");
                Debug.WriteLine(remote);

                Debug.Write("\nВаш выбор: ");
                var input = "2";// Debug.ReadLine();
                int buttonId;
                int.TryParse(input, out buttonId);

                remote.PushButton(buttonId);
                remote.UndoButton(buttonId);

                Debug.Write("\nВы хотите продолжить (y/n): ");
                userInput = "n";//Debug.ReadLine();
            } while (userInput == "y");
        }
    }
}
