using System.Diagnostics;
using Patterns._03_Behavioral.Strategy._001_Ducks.Fly;

namespace Patterns._03_Behavioral.Strategy._001_Ducks.Ducks
{
    public class RubberDuck : DuckBase
    {
        public RubberDuck()
        {
            flyBehaviour = new NoFly();
        }

        public override void Display()
        {
            Debug.WriteLine("Hi! I'm a rubber duck!");
        }
    }
}
