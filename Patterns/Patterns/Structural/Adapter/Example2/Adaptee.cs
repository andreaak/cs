using System;

namespace Patterns.Structural.Adapter.Example2
{
    class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Adaptee.SpecificRequest");
        }
    }
}
