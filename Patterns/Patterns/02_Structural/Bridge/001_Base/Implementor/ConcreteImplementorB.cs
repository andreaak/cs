using System.Diagnostics;

namespace Patterns._02_Structural.Bridge._001_Base.Implementor
{
    class ConcreteImplementorB : Implementor
    {
        public override void OperationImp()
        {
            Debug.WriteLine("ConcreteImplementorB");
        }
    }
}
