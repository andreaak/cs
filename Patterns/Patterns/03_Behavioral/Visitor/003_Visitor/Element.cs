namespace Patterns._03_Behavioral.Visitor._003_Visitor
{
    abstract class Element
    {
        public abstract void Accept(Visitor visitor);
        public string SomeState { get; set; }
    }
}
