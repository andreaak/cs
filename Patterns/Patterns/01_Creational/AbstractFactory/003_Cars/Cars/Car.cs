using System;
using Patterns.Creational.AbstractFactory._003_Cars.Parts;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory._003_Cars.Cars
{
    public abstract class Car
    {
        protected string Name = "";
        protected string Body = "Caravan";

        protected Engine Engine;
        protected Paint PaintColor;
        protected Wheels Wheels;

        public abstract void Configure();

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
