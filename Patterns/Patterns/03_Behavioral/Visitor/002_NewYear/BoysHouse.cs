using System.Diagnostics;

namespace Patterns._03_Behavioral.Visitor._002_NewYear
{
    class BoysHouse : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitBoysHouse(this);
        }

        public void TellFairyTale()
        {
            Debug.WriteLine("Fairy Tale....");
        }
    }
}
