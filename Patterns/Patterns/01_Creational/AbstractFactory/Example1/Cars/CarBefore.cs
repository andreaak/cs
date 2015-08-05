using System;
using Patterns.Creational.AbstractFactory.Example1.Parts;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory.Example1.Cars
{
    public class CarBefore
    {
        protected string Name = "";
        protected string Body = "Caravan";

        protected string Engine = "Diesel";
        protected string PaintColor = "White";
        protected string Wheels = "16 inch";

        public void Configure()
        {
            Console.WriteLine("Configure " + Name);
            Console.WriteLine("Body is " + Body);
            Console.WriteLine("Engine is " + Engine);
            Console.WriteLine("PaintColor is " + PaintColor);
            Console.WriteLine("Wheels is " + Wheels);
        }

        public void AssembleBody()
        {
            Console.WriteLine("Body is assembled");
        }

        public void InstallEngine()
        {
            Console.WriteLine("Engine is in its place");
        }

        public void Paint()
        {
            Console.WriteLine("Car is painted");
        }

        public void InstallWheels()
        {
            Console.WriteLine("Wheels are installed");
        }
    }
}
