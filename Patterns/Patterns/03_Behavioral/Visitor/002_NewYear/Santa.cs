﻿
namespace Patterns._03_Behavioral.Visitor._002_NewYear
{
    class Santa : Visitor
    {
        public override void VisitBoysHouse(BoysHouse boysHouse)
        {
            boysHouse.TellFairyTale();
        }

        public override void VisitGirlsHouse(GirlsHouse girlsHouse)
        {
            girlsHouse.GiveDress();
        }
    }
}
