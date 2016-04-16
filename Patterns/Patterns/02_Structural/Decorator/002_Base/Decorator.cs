using System;

namespace Patterns._02_Structural.Decorator._002_Base
{
    abstract class Decorator : Component
    {
        public Component Component { protected get; set; }

        public override void Operation()
        {
            if (Component != null)
                Component.Operation();
        }
    }
}
