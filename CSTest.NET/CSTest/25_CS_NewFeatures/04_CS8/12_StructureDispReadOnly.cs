using CSTest._04_Class._13_AnonymousTypes;
using NUnit.Framework;

namespace CSTest._25_CS_NewFeatures._04_CS8
{
#if CS7

    [TestFixture]
    public class _12_Structure
    {

        [Test]
        public void TestStructure_1()
        {
            var str1 = new Point5(1, 4);
            //str1.X = 5;
            //str1.XX = 5; //Error
            /*

            */
            List<Point5> points = new List<Point5>
            {
                new Point5(1, 2)
            };
            foreach (var p in points)
            {
                //p.X = 3; Чтобы избежать логической ошибки (когда программист думает, что изменяет список), компилятор просто запрещает это.
            }

            Point5[] points2 = new Point5[10];
            foreach (Point5 p in points2)
            {
                //p.X = 3;
            }
        }
        
        [Test]
        public void TestStructure_2()
        {
            var s = new DisposableRefStruct(50, 60);
            s.Display();
            s.Dispose();
        }
    }

    /*
    В версии C# 8.0 появилась возможность объявления индивидуальных полей струк-
    туры как readonly. Это обеспечивает более высокий уровень детализации , чем объ-
    явление целой структуры как допускающей только чтение. Модификатор readonly
    может применяться к методам , свойствам и средствам доступа для свойств.  
    */

    struct Point5
    {
        public readonly int X;
        public readonly int Y;

        public Point5(int x, int y)
        {
            X = x;
            Y = y;
        }

        //public readonly void ResetX() => X = 0; //Ошибка ! 
        public readonly int ResetX() => X;
    }

    /*
    Как было указано в предыдущем разделе , структуры ref (и структуры ref, допус-
    кающие только чтение) не могут реализовывать интерфейсы , а потому реализовать
    IDisposable нельзя. В версии C# 8.0 появилась возможность делать структуры ref
    и структуры ref, допускающие только чтение , освобождаемыми, добавляя открытый
    метод void Dispose().
     */
    ref struct DisposableRefStruct
    {
        public int X;
        public readonly int Y;
        public readonly void Display()
        {
            Console.WriteLine($"X = {X}, Y = {Y}");
        }
        // Специальный конструктор.
        public DisposableRefStruct(int xPos, int yPos)
        {
            X = xPos;
            Y = yPos;
            Console.WriteLine("Created!"); // Экземпляр создан!
        }
        public void Dispose()
        {
            // Выполнить здесь очистку любых ресурсов.
            Console.WriteLine("Disposed!"); // Экземпляр освобожден!
        }
    }


#endif

}
