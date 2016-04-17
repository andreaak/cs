using System.Diagnostics;

namespace Patterns._03_Behavioral.Strategy._003_StrategySort.Strategy
{
    // ����������� ����������.
    class BubbleSort : Strategy
    {
        public override void Sort(ref int[] array)
        {
            Debug.WriteLine("BubbleSort");

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = array.Length - 1; j > i; j--)
                {
                    if (array[j] < array[j - 1])
                    {
                        int temp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = temp;
                    }
                }
            }
        }
    }
}
