using System.Diagnostics;
using Patterns._01_Creational.AbstractFactory._003_Cars.PartsFactory;

namespace Patterns._01_Creational.AbstractFactory._003_Cars.Cars
{
    public class Golf : Car
    {
        private CarPartsFactory _factory;

        public Golf(CarPartsFactory factory)
        {
            Name = "Golf";
            Body = "Hatchback";

            _factory = factory;
        }

        public override void Configure()
        {
            Debug.WriteLine("Configuring " + Name);
            Debug.WriteLine("Body is: " + Body);

            Engine = _factory.CreateEngine();
            PaintColor = _factory.CreatePaint();
            Wheels = _factory.CreateWheels();
        }
    }
}
