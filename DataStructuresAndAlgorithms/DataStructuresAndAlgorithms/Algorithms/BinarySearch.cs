using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Algorithms
{
    [TestClass]
    public class BinarySearch
    {
        /*
        При двоичном поиске центральный элемент отсортированной области поиска
        (в данном случае — массив) сравнивается с элементом, который требуется най­ти. 
        Существует три возможности: 
            Если центральный элемент меньше целевого, убирается первая половина области поиска. 
            Если он больше, убирается вторая половина области поиска. 
            В третьем случае, когда центральный элемент равен целевому, поиск прекращается. 
        Процесс повторяется с оставшейся частью об­ласти поиска до наступления третьего случая.
        Время двоичного поиска оценивается как O(log(n)), так как на каждой итерации
        у нас исчезает половина области поиска. 
        Это более эффективно, чем обычный поиск по элементам, который оценивается как O(n). 
        Но двоичный поиск воз­можен только в отсортированном массиве, 
        а время сортировки обычно оцени­вается как O(n*log(n)).
        
        */
        [TestMethod]
        public void Test()
        {

        }

        int IterBinarySearch(int[] array, int target)
        {
            int lower = 0, upper = array.Length - 1;
            int center, range;
            if (lower > upper)
            {
                throw new Exception("Пределы поменялись местами");
            }

            while (true)
            {
                range = upper - lower;
                if (range == 0 && array[lower] != target)
                {
                    throw new Exception("Элемент в массиве отсутствует");
                }
                if (array[lower] > array[upper])
                {
                    throw new Exception("Maccив не отсортирован");
                }

                center = range / 2 + lower;
                if (target == array[center])
                {
                    return center;
                }
                else if (target < array[center])
                {
                    upper = center - 1;
                }
                else
                {
                    lower = center + 1;
                }
            }
        }



    }
}
