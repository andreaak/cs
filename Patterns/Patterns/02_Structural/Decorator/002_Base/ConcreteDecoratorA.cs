using System.Diagnostics;

namespace Patterns._02_Structural.Decorator._002_Base
{
    class ConcreteDecoratorA : Decorator
    {
        string addedState = "Some State";

        public override void Operation()
        {
            base.Operation();
            Debug.WriteLine(addedState);
        }
    }
}
