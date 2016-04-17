using System.Diagnostics;

namespace Patterns._03_Behavioral.TemplateMethod._003_TemplateMethod
{
    class UkraineFlag :TwoColorFlag
    {
        protected override void DrawTopPart()
        {
            //Debug.BackgroundColor = ConsoleColor.Blue;
            Debug.WriteLine(new string(' ', 7));
        }

        protected override void DrawBottomPart()
        {
            //Debug.BackgroundColor = ConsoleColor.Yellow;
            Debug.WriteLine(new string(' ', 7));
        }
    }
}
