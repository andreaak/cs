using System.Diagnostics;

namespace Patterns._01_Creational.AbstractFactory._003_Cars.Parts
{
    public class WhitePaint : Paint
    {
        public WhitePaint()
        {
            Debug.WriteLine("Paint is White");
        }
    }
}
