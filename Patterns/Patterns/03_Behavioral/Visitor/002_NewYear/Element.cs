
namespace Patterns._03_Behavioral.Visitor._002_NewYear
{
    abstract class Element
    {
        public abstract void Accept(Visitor visitor);
    }
}
