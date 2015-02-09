using System;
using System.Diagnostics;

namespace Patterns.Structural.Facade.Example1.WashingMachine
{
    class Thermo
    {
        public int GetTemperature()
        {
            return 50;
        }

        public void WarmUp(int degrees)
        {
            Debug.WriteLine("Warm for {0} degrees", degrees);
        }
    }
}
