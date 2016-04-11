using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Creational.Builder._003_Cars.Builder
{
    public class VolkswagenBuilder : CarBuilderBase
    {
        public VolkswagenBuilder()
            : base()
        {
        }

        public override void BuildMultimedia()
        {
            car.Multimedia = "VW RNS 510";
        }

        public override void BuildWheels()
        {
            car.Wheels += " 17\" VW Wheel";
        }

        public override void BuildEngine()
        {
            car.Engine = "1.8 TSI";
        }

        public override void BuildFrames()
        {
            car.Frame = "VW frame";
        }

        public override void BuildLuxury()
        {
            car.Luxury = "VW R-style";
        }

        public override void BuildSafety()
        {
            car.Safety = "VW Lane Assist";
        }
    }
}
