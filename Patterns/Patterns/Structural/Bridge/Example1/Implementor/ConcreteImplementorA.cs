using System;

namespace Patterns.Structural.Bridge.Example1
{
    class ConcreteImplementorA : Implementor
    {
        public override void OperationImp()
        {
            Console.WriteLine("ConcreteImplementorA");
        }
    }
}
