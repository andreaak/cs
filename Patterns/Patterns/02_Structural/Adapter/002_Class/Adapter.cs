using System;

namespace Patterns.Structural.Adapter._002_Class
{
    class Adapter : Adaptee, ITarget
    {
        public void Request()
        {
            base.SpecificRequest();
        }
    }
}
