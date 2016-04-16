using System.Diagnostics;

namespace Patterns._02_Structural.Facade._001_WashingMachine.WashingMachine
{
    class Engine
    {
        public void Rotate()
        {
            Debug.WriteLine("Start rotating");
        }

        public void Stop()
        {
            Debug.WriteLine("Stop rotating");
        }
    }
}
