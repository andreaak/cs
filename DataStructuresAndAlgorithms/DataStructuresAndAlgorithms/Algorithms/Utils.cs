using System.Diagnostics;
using DataStructuresAndAlgorithms.Algoritms.SelectionSort;

namespace DataStructuresAndAlgorithms.Algoritms
{
    public static class Utils
    {
        public static void PrintValues(this int[] arr)
        {
            foreach (int i in arr)// O(n)
            {
                Debug.Write(i.ToString() + ' ');
            }
            Debug.WriteLine("");
        }

        public static void PrintValues(this SelectionUnit[] arr)
        {
            foreach (SelectionUnit i in arr)// O(n)
            {
                Debug.Write(i.ToString() + "; ");
            }
            Debug.WriteLine("");
        }

        public static void Swap(this int[] items, int left, int right)
        {
            if (left != right)
            {
                int temp = items[left];// O(1)
                items[left] = items[right];// O(1)
                items[right] = temp;// O(1)
            }
        }
    }

    public struct SelectionUnit
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return string.Format("{0}.{1}", X, Y);
        }

    }
}
