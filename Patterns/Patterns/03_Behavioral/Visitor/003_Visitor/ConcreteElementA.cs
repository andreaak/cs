using System.Diagnostics;

namespace Patterns._03_Behavioral.Visitor._003_Visitor
{
    class ElementA : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitElementA(this);
        }

        public void OperationA()
        {
            Debug.WriteLine("OperationA");
        }
    }
}
