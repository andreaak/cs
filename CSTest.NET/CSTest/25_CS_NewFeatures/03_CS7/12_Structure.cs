using NUnit.Framework;
using System;

namespace CSTest._25_CS_NewFeatures._03_CS7
{
#if CS7

    [TestFixture]
    public class _12_Structure
    {

        [Test]
        public void TestStructure_1()
        {
            var str1 = new Point4(1, 4);
            var str2 = new Point4
            {
                XX = 4,
                //YY = 5 //Error
            };

            //str1.X = 5;
            //str1.XX = 5; //Error
            /*

            */
        }
    }
    /*
    Структуры можно также помечать как допускающие только чтение, если необходи-
    мо, чтобы они были неизменяемыми.Неизменяемые объекты должны устанавливаться
    при конструировании и поскольку изменять их нельзя, они могут быть более произ-
    водительными. В случае объявления структуры как допускающей только чтение все
    свойства тоже должны быть доступны только для чтения.
     
     
     */
    readonly struct Point4
    {
        public readonly int X; //Х и У должны быть readonly 
        public readonly int Y; //Х и У должны быть readonly 
        

        public int XX { get; init; }
        public int YY { get; }

        public Point4(int x, int y)
        {
            X = x;
            Y = y;
            XX = 3;
            YY = 4;
        }

    }

    interface TestPoint4
    {

    }

    public class Point5Cl
    {
        //public Point5 Z; //Field or auto-implemented property cannot be of type 'Point5' unless it is an instance member of a ref struct.
    }

    /*
    При определении структуры в C# 7.2 также появилась возможность применения
    модификатора ref. Он требует, чтобы все экземпляры структуры находились в стеке
    и не могли присваиваться свойству другого класса. Формальная причина для этого за-
    ключается в том, что ссылки на структуры ref из кучи невозможны . Отличие между
    стеком и кучей объясняется в следующем разделе.
    Ниже перечислены дополнительные ограничения структур ref:
    • их нельзя присваивать переменной типа object или dynamic, и они не могут
    быть интерфейсного типа;
    • они не могут реализовывать интерфейсы ;
    • они не могут использоваться в качестве свойства структуры , не являющейся ref ;
    • они не могут применяться в асинхронных методах , итераторах, лямбда-выраже-
    ниях или локальных функциях.
     */

    ref struct Point5 //: TestPoint4 Error
    {
        public readonly int X; //Х и У должны быть readonly 
        public readonly int Y; //Х и У должны быть readonly 

        public Point5(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

#endif
}
