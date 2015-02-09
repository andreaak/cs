using System;
using System.Diagnostics;

namespace Patterns.Structural.Facade.Example1.WashingMachine
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
