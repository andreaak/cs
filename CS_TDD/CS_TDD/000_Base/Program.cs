using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._000_Base
{
    class Program
    {
        static void Main()
        {
            Calculator c = new Calculator();

            Console.WriteLine("3 + 5 = 8", c.Add(3, 5));
            Console.WriteLine("6 - 2 = 4", c.Sub(6, 2));
            Console.WriteLine("8 * 3 = 24", c.Mul(8, 3));
            Console.WriteLine("6 / 3 = 2", c.Div(6, 3));

            Console.ReadKey();
        }
    }
}
