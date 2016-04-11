using System.Diagnostics;

namespace Patterns.Structural.Bridge._001_Base
{
    class ConcreteImplementorB : Implementor
    {
        public override void OperationImp()
        {
            Debug.WriteLine("ConcreteImplementorB");
        }
    }
}
