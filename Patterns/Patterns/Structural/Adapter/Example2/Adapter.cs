using System;

namespace Patterns.Structural.Adapter.Example2
{
    class Adapter : Adaptee, ITarget
    {
        public void Request()
        {
            base.SpecificRequest();
        }
    }
}
