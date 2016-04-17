namespace Patterns._03_Behavioral.Visitor._001_Base
{
    abstract class Visitor
    {
        public abstract void VisitConcreteElementA(ConcreteElementA elementA);
        public abstract void VisitConcreteElementB(ConcreteElementB elementB);
    }
}
