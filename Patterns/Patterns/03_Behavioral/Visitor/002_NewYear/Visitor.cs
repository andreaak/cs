
namespace Patterns._03_Behavioral.Visitor._002_NewYear
{
    abstract class Visitor
    {
        public abstract void VisitBoysHouse(BoysHouse boysHouse);
        public abstract void VisitGirlsHouse(GirlsHouse girlsHouse);
    }
}
