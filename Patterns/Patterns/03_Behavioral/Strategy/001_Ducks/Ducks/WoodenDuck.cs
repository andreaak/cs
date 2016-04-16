using System.Diagnostics;
using Patterns._03_Behavioral.Strategy._001_Ducks.Fly;
using Patterns._03_Behavioral.Strategy._001_Ducks.Quack;

namespace Patterns._03_Behavioral.Strategy._001_Ducks.Ducks
{
    public class WoodenDuck : DuckBase
    {
        public WoodenDuck()
        {
            flyBehaviour = new NoFly();
            quackBehaviour = new NoQuack();
        }

        public override void Display()
        {
            Debug.WriteLine("Hi! I'm a wooden duck!");
        }
    }
}
