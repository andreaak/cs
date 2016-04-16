using System.Diagnostics;

namespace Patterns._03_Behavioral.Strategy._001_Ducks.Quack
{
    public class SimpleQuack : IQuackable
    {
        public void Quack()
        {
            Debug.WriteLine("Quack! Quack!");
        }
    }
}
