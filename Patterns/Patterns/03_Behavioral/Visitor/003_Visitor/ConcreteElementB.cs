using System.Diagnostics;

namespace Patterns._03_Behavioral.Visitor._003_Visitor
{
    class ElementB : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitElementB(this);
        }

        public void OperationB()
        {
            Debug.WriteLine("OperationB");
        }
    }
}
