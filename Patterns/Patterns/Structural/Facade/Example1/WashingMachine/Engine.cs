using System;
using System.Diagnostics;

namespace Patterns.Structural.Facade.Example1.WashingMachine
{
    class Engine
    {
        public void Rotate()
        {
            Console.WriteLine("Start rotating");
        }

        public void Stop()
        {
            Console.WriteLine("Stop rotating");
        }
    }
}
