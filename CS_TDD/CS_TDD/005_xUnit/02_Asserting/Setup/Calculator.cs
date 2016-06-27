using System;

namespace CS_TDD._005_xUnit._02_Asserting.Setup
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }


        public double Add(double a, double b)
        {
            return a + b;
        }

        public int Divide(int value, int by)
        {
            if (value > 200) // for demo purposes
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return value / by;
        }
    }
}