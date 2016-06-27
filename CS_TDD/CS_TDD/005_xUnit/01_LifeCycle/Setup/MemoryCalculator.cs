using System;
using System.Threading;

namespace CS_TDD._005_xUnit._01_LifeCycle.Setup
{
    public class MemoryCalculator : IDisposable
    {
        public int CurrentValue { get; private set; }

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

        public void Subtract(int number)
        {
            CurrentValue -= number;
        }

        public void Divide(int number)
        {
            CurrentValue = CurrentValue / number;
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
