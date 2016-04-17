using System.Diagnostics;

namespace Patterns._03_Behavioral.Visitor._001_Base
{
    class ConcreteElementB : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementB(this);
        }

        public void OperationB()
        {
            Debug.WriteLine("OperationB");
        }
    }
}
