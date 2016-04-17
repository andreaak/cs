namespace Patterns._03_Behavioral.Visitor._003_Visitor
{
    abstract class Visitor
    {
        public abstract void VisitElementA(ElementA elementA);
        public abstract void VisitElementB(ElementB elementB);
    }
}
