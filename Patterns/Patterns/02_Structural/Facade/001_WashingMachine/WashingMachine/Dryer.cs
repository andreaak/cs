using System.Diagnostics;

namespace Patterns._02_Structural.Facade._001_WashingMachine.WashingMachine
{
    class Dryer
    {
        public void Dry(int seconds, int intensity)
        {
            Debug.WriteLine("Drying {0} seconds with intensity {1}", seconds, intensity);
        }
    }
}
