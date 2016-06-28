using System;
using System.Threading;

namespace CS_TDD._000_Base
{
    public class MemoryCalculator : IDisposable
    {
        public int CurrentValue { get;  set; }

        public MemoryCalculator()
        {
        }

        public MemoryCalculator(bool isExpensive)
        {
            // Simulate expensive object creation for 
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        public void Add(int number)
        {
            CurrentValue += number;
        }

        public void Sub(int number)
        {
            CurrentValue -= number;
        }

        public void Div(int number)
        {
            CurrentValue = CurrentValue / number;
        }

        public int Add(int x, int y)
        {
            return x + y;
        }

        public double Add(double x, double y)
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
            if (x > 200)
            {
                throw new ArgumentOutOfRangeException("value");
            }
            
            // При попытке деления на нуль генерируется исключительная ситуация
            if (y == 0)
            {
                throw new DivideByZeroException("Попытка деления на нуль.");
            }
            return x / y;
        }

        public void Dispose()
        {
            // Clean up resources
        }

        public void Clear()
        {
            CurrentValue = 0;
        }
    }
}
