using Patterns.Creational.AbstractFactory._003_Cars.Parts;

namespace Patterns.Creational.AbstractFactory._003_Cars.PartsFactory
{
    public class RussianCarPartsFactory : CarPartsFactory
    {
        public override Engine CreateEngine()
        {
            return new GasolineEngine();
        }

        public override Paint CreatePaint()
        {
            return new BlackPaint();
        }

        public override Wheels CreateWheels()
        {
            return new MediumWheels();
        }
    }
}
