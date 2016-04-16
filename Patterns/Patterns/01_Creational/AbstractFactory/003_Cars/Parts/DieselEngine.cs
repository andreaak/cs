using System.Diagnostics;

namespace Patterns._01_Creational.AbstractFactory._003_Cars.Parts
{
    public class DieselEngine : Engine
    {
        public DieselEngine()
        {
            Debug.WriteLine("Engine is Diesel");
        }
    }
}
