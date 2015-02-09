//Если один метод вызывает другой, то необходимо более тщательно оценить сложность последнего. 
//Если в нем выполняется определённое число простых инструкций, то на оценку сложности это практически не влияет. 
//Если же метод вызывается внутри цикла, то влияние может быть намного больше.

//В качестве примера рассмотрим два метода: DoSlowly со сложностью O(n^3) и DoFastly со сложностью O(n^2).

//Если во внутренних циклах процедуры Fast происходит вызов процедуры Slow, то сложности процедур перемножаются. 
//В данном случае сложность алгоритма составляет O(n^2 )*O(n^3 )=O(n^5)

using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Base
{
    [TestClass]
    public class Multiplication
    {
        [TestMethod]
        public void Test1()
        {
            DoFastly(1, 1);

            Debug.WriteLine("Done");
        }

        // Степень роста метода DoFastly O(n^3)
        static void DoSlowly(int inum, int jnum, int knum)
        {
            int a = 0;

            for (int i = 0; i < inum; i++)
            {
                for (int j = 0; j < jnum; j++)
                {
                    for (int k = 0; k < knum; k++)
                    {
                        a++;
                    }
                }
            }

            Debug.WriteLine("a = {0}", a);
        }

        // Степень роста метода DoFastly O(n^2)
        static void DoFastly(int inum, int jnum)
        {

            for (int i = 0; i < inum; i++)
            {
                for (int j = 0; j < jnum; j++)
                {
                    DoSlowly(5, 6, 7);
                }
            }
        }
 
    }
}