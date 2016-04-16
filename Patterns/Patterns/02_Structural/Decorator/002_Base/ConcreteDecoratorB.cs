using System.Diagnostics;

namespace Patterns._02_Structural.Decorator._002_Base
{
    class ConcreteDecoratorB : Decorator
    {
        void AddedBehavior()
        {
            Debug.WriteLine("AddedBehavior");
        }

        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
        }
    }
}
