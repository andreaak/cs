using System.Diagnostics;

namespace Patterns._03_Behavioral.Strategy._001_Ducks.Ducks
{
    public class SimpleDuck : DuckBase
    {
        public override void Display()
        {
            Debug.WriteLine("Hi! I'm a simle duck.");
        }
    }
}
