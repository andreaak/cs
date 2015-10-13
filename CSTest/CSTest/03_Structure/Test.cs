using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace CSTest._03_Structure
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestStructure1()
        {
            MyStruct mstr;
            mstr.field = 1;
            Debug.WriteLine(mstr.field);
        }

        [TestMethod]
        public void TestStructure2()
        {
            MyStruct mstr = new MyStruct();
            Debug.WriteLine(mstr.field);
        }

        [TestMethod]
        public void TestStructure3()
        {
            MyStruct1 mstr;
            mstr.field = 1;
            Debug.WriteLine(mstr.field);
        }

        [TestMethod]
        public void TestStructure4()
        {
            MyStruct1 mstr = new MyStruct1();
            Debug.WriteLine(mstr.field);
        }

        [TestMethod]
        public void TestStructure5()
        {
            MyStruct mstr = new MyStruct();
            ValueType valueType = mstr;
            Debug.WriteLine("instance = " + mstr.GetHashCode());
            Debug.WriteLine("valueType = " + valueType.GetHashCode());

        }

        [TestMethod]
        public void TestStructure6()
        {
            var p = new Point(); p.x = 5; p.y = 9;
            var list = new System.Collections.Generic.List<Point>(10);
            list.Add(p);
            //list[0].x = 90;//ошибка компиляции

            var array = new Point[10];
            array[0] = p;
            array[0].x = 90;//все ок
            /*В первом случае мы получим ошибку компиляции, 
             поскольку индексатор коллекции это всего-навсего метод, 
             который возвращает копию нашей структуры. 
             Во втором случае мы ошибки не получим, поскольку 
             индексация в массивах это не вызов метода, 
             а обращение именно к нужному элементу.*/
        }
    }

    struct Point
    {
        public int x;
        public int y;
    }
}
