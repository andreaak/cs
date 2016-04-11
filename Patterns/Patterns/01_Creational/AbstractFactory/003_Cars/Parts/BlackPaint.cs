using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Creational.AbstractFactory._003_Cars.Parts
{
    public class BlackPaint : Paint
    {
        public BlackPaint()
        {
            Debug.WriteLine("Paint is Black");
        }
    }
}
