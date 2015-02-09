using System;

namespace Patterns.Structural.Adapter.Example3
{
    class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Adaptee.SpecificRequest");
        }
    }
}
