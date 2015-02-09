using System;

namespace Patterns.Structural.Bridge.Example1
{
    class ConcreteImplementorB : Implementor
    {
        public override void OperationImp()
        {
            Console.WriteLine("ConcreteImplementorB");
        }
    }
}
