using System.Diagnostics;

namespace Patterns._03_Behavioral.TemplateMethod._003_TemplateMethod
{
    class PolandFlag : TwoColorFlag
    {
        protected override void DrawTopPart()
        {
            //Console.BackgroundColor = ConsoleColor.White;
            Debug.WriteLine(new string(' ', 7));
        }

        protected override void DrawBottomPart()
        {
            //Console.BackgroundColor = ConsoleColor.Red;
            Debug.WriteLine(new string(' ', 7));
        }
    }
}
