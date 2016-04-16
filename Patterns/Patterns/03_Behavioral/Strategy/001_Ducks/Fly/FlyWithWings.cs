using System.Diagnostics;

namespace Patterns._03_Behavioral.Strategy._001_Ducks.Fly
{
    public class FlyWithWings : IFlyable
    {
        public void Fly()
        {
            Debug.WriteLine("I'm flying with my wings");
        }
    }
}
