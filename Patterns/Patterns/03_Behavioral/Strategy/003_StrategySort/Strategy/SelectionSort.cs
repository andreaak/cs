using System.Diagnostics;

namespace Patterns._03_Behavioral.Strategy._003_StrategySort.Strategy
{
    // Сортировка выбором.
    class SelectionSort : Strategy
    {
        public override void Sort(ref int[] array)
        {
            Debug.WriteLine("SelectionSort");

            for (int i = 0; i < array.Length - 1; i++)
            {
                int k = i;

                for (int j = i + 1; j < array.Length; j++)
                    if (array[k] > array[j])
                        k = j;

                if (k != i)
                {
                    int temp = array[k];
                    array[k] = array[i];
                    array[i] = temp;
                }
            }
        }
    }
}
