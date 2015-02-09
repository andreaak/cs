using System;

namespace Patterns.Structural.Bridge.Example1
{
    abstract class Abstraction
    {
        protected Implementor implementor = null;

        public Abstraction(Implementor implementor)
        {
            this.implementor = implementor;
        }

        public virtual void Operation()
        {
            implementor.OperationImp();
        }
    }
}
