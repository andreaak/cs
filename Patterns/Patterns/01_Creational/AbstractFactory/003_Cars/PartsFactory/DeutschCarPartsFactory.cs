using Patterns._01_Creational.AbstractFactory._003_Cars.Parts;

namespace Patterns._01_Creational.AbstractFactory._003_Cars.PartsFactory
{
    public class DeutschCarPartsFactory : CarPartsFactory
    {
        public override Engine CreateEngine()
        {
            return new DieselEngine();
        }

        public override Paint CreatePaint()
        {
            return new WhitePaint();
        }

        public override Wheels CreateWheels()
        {
            return new BigWheels();
        }
    }
}
