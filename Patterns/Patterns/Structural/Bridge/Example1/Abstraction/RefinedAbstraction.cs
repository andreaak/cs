using System;

namespace Patterns.Structural.Bridge.Example1
{
    class RefinedAbstraction : Abstraction
    {
        public RefinedAbstraction(Implementor implementor)
            : base(implementor)
        {
        }

        public override void Operation()
        {
            // ...
            base.Operation();
            // ...
        }
    }
}
