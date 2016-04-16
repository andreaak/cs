using System.Diagnostics;

namespace Patterns._02_Structural.Decorator._002_Base
{
    class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Debug.WriteLine("ConcreteComponent.Operation()");
        }
    }
}
