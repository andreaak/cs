using System.Diagnostics;

namespace Patterns._01_Creational.AbstractFactory._003_Cars.Parts
{
    public class GasolineEngine : Engine
    {
        public GasolineEngine()
        {
            Debug.WriteLine("Engine is gasoline");
        }
    }
}
