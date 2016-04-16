using System.Diagnostics;

namespace Patterns._01_Creational.AbstractFactory._003_Cars.Parts
{
    public class BlackPaint : Paint
    {
        public BlackPaint()
        {
            Debug.WriteLine("Paint is Black");
        }
    }
}
