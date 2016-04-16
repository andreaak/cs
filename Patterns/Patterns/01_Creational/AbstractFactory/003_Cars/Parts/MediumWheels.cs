using System.Diagnostics;

namespace Patterns._01_Creational.AbstractFactory._003_Cars.Parts
{
    public class MediumWheels : Wheels
    {
        public MediumWheels()
        {
            Debug.WriteLine("Wheels are 16\"");
        }
    }
}
