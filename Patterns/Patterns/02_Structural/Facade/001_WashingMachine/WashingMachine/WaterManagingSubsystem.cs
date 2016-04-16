using System.Diagnostics;

namespace Patterns._02_Structural.Facade._001_WashingMachine.WashingMachine
{
    class WaterManagingSubsystem
    {
        public void FillWater(int litres)
        {
            Debug.WriteLine("Fill with {0} litres of water", litres);
        }

        public void EmptyWater()
        {
            Debug.WriteLine("Empty water tank");
        }
    }
}
