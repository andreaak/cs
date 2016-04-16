using System.Diagnostics;

namespace Patterns._03_Behavioral.Strategy._001_Ducks.Fly
{
    public class NoFly : IFlyable
    {
        public void Fly()
        {
            Debug.WriteLine("---");
        }
    }
}
