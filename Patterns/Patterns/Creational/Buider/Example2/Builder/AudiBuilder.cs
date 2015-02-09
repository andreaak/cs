using System;

namespace Creational.Builder.Example2.Builder
{
    public class AudiBuilder : CarBuilderBase
    {
        public AudiBuilder() : base()
        {
        }

        public override void BuildMultimedia()
        {
            car.Multimedia = "Audi MMI Multimedia";
        }

        public override void BuildWheels()
        {
            car.Wheels += " 18\" Audi Wheel";
        }

        public override void BuildEngine()
        {
            car.Engine = "2.0 TFSI";
        }

        public override void BuildFrames()
        {
            car.Frame = "Audi frame";
        }

        public override void BuildLuxury()
        {
            car.Luxury = "Audi Exclusive Interior";
        }

        public override void BuildSafety()
        {
            car.Safety = "Side Assist";
        }
    }
}
