using System.Diagnostics;

namespace Patterns._03_Behavioral.Visitor._002_NewYear
{
    class GirlsHouse : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitGirlsHouse(this);
        }

        public void GiveDress()
        {
            Debug.WriteLine("Dress as a gift.");
        }
    }
}
