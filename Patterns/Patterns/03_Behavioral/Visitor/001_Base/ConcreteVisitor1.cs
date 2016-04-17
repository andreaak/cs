namespace Patterns._03_Behavioral.Visitor._001_Base
{
    class ConcreteVisitor1 : Visitor
    {
        public override void VisitConcreteElementA(ConcreteElementA elementA)
        {
            elementA.OperationA();
        }

        public override void VisitConcreteElementB(ConcreteElementB elementB)
        {
            elementB.OperationB();
        }
    }
}
