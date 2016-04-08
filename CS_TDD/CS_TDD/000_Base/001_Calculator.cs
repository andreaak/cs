using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._000_Base
{
    class Calculator
    {
        int x, y;

        public Calculator()
        {

        }

        public Calculator(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Sub(int x, int y)
        {
            return x - y;
        }

        public int Mul(int x, int y)
        {
            return x * y;   // Логическая ошибка.
        }

        public int Div(int x, int y)
        {
            // При попытке деления на нуль генерируется исключительная ситуация
            if (y == 0)
            {
                throw new DivideByZeroException("Попытка деления на нуль.");
            }
            return x / y;
        }
    }
}
