using NUnit.Framework;
using System;

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
        }
    }

    struct Point5
    {
        public int X, Y;
        public Point5(int x, int y)
        {
            X = x;
            Y = y;
        }

        //public readonly void ResetX() => X = 0; //Ошибка ! 
        public readonly int ResetX() => X; 
    }

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
