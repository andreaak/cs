using System.Diagnostics;

namespace Patterns._03_Behavioral.Strategy._003_StrategySort
{
    class Context
    {
        Strategy.Strategy strategy;
        int[] array = { 3, 5, 1, 2, 4 };

        public Context(Strategy.Strategy strategy)
        {
            this.strategy = strategy;
        }

        public void Sort()
        {
            strategy.Sort(ref array);
        }

        public void PrintArray()
        {
            for (int i = 0; i < array.Length; i++)
                Debug.Write(array[i] + " ");

            Debug.WriteLine("");
        }
    }
}
