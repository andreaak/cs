using System.Diagnostics;

namespace Patterns.Creational.SimpleFactory._001_Base.Cars
{
    public class Car
    {
        protected string Name = "";
        protected string Body = "Caravan";

        protected string Engine = "Diesel";
        protected string PaintColor = "White";
        protected string Wheels = "16 inch";

        public void Configure()
        {
            Debug.WriteLine("Configure " + Name);
            Debug.WriteLine("Body is " + Body);
            Debug.WriteLine("Engine is " + Engine);
            Debug.WriteLine("PaintColor is " + PaintColor);
            Debug.WriteLine("Wheels is " + Wheels);
        }

        public void AssembleBody()
        {
            Debug.WriteLine("Body is assembled");
        }

        public void InstallEngine()
        {
            Debug.WriteLine("Engine is in its place");
        }

        public void Paint()
        {
            Debug.WriteLine("Car is painted");
        }

        public void InstallWheels()
        {
            Debug.WriteLine("Wheels are installed");
        }
    }
}
