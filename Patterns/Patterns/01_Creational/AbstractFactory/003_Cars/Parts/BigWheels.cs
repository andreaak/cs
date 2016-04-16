using System.Diagnostics;

namespace Patterns._01_Creational.AbstractFactory._003_Cars.Parts
{
    public class BigWheels : Wheels
    {
        public BigWheels()
        {
            Debug.WriteLine("Wheels are 17\"");
        }
    }
}
