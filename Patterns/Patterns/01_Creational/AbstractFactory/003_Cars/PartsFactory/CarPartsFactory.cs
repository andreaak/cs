using Patterns._01_Creational.AbstractFactory._003_Cars.Parts;

namespace Patterns._01_Creational.AbstractFactory._003_Cars.PartsFactory
{
    public abstract class CarPartsFactory
    {
        public abstract Engine CreateEngine();
        public abstract Paint CreatePaint();
        public abstract Wheels CreateWheels();
    }
}
