using System.Diagnostics;

namespace Patterns._03_Behavioral.Strategy._001_Ducks.Quack
{
    public class NoQuack : IQuackable
    {
        public void Quack()
        {
            Debug.WriteLine("...");
        }
    }
}
