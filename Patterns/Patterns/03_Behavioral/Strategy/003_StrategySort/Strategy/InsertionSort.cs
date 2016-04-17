using System.Diagnostics;

namespace Patterns._03_Behavioral.Strategy._003_StrategySort.Strategy
{
    // Сортировка вставками.
    class InsertionSort : Strategy
    {
        public override void Sort(ref int[] array)
        {
            Debug.WriteLine("InsertionSort");

            for (int i = 1; i < array.Length; i++)
            {
                int j = 0;
                int buffer = array[i];

                for (j = i - 1; j >= 0; j--)
                {
                    if (array[j] < buffer)
                        break;

                    array[j + 1] = array[j];
                }

                array[j + 1] = buffer;
            }
        }
    }
}
