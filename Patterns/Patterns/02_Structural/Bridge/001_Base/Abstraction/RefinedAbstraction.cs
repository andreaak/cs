using System;

namespace Patterns.Structural.Bridge._001_Base
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
