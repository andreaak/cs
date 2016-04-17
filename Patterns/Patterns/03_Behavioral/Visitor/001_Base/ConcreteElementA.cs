using System.Diagnostics;

namespace Patterns._03_Behavioral.Visitor._001_Base
{
    class ConcreteElementA : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementA(this);
        }

        public void OperationA()
        {
            Debug.WriteLine("OperationA");
        }
    }
}
