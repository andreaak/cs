//Если основная программа вызывает методы по очереди, то их сложности складываются: O(n^2 )+O(n^3)=O(n^3). 

using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Base
{
    [TestClass]
    public class Sum
    {
        [TestMethod]
        public void Test1()
        {
            DoSlowly(2, 16, 9);
            DoFastly(2, 2);

            Debug.WriteLine("Done");
        }

        // Степень роста метода DoSlowly O(n^3)  
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
            int b = 0;

            for (int i = 0; i < inum; i++)
            {
                for (int j = 0; j < jnum; j++)
                {
                    b++;
                }
            }
        }
 
    }
}