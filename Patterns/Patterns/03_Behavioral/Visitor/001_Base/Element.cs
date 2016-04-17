namespace Patterns._03_Behavioral.Visitor._001_Base
{
    abstract class Element
    {
        public abstract void Accept(Visitor visitor);
    }
}
