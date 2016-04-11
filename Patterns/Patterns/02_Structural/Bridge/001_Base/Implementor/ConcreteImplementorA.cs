using System.Diagnostics;

namespace Patterns.Structural.Bridge._001_Base
{
    class ConcreteImplementorA : Implementor
    {
        public override void OperationImp()
        {
            Debug.WriteLine("ConcreteImplementorA");
        }
    }
}
