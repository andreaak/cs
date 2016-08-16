using NUnit.Framework;

namespace Patterns._01_Creational.FactoryMethod._007_FactoryMethodInConstructor
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Creator creator = new ConcreteCreator();

            // В этом примере на экран будет выведено: 
            // SpecialProduct  -  работа конструктора базового класса
            // SpecialProduct  -  работа конструктора производного класса

            // Delay.
            //Debug.ReadKey();

            Creator2 creator2 = new ConcreteCreator2();

            // В этом примере на экран будет выведено: 
            // DefaultProduct  -  работа конструктора базового класса
            // SpecialProduct  -  работа конструктора производного класса

            // Delay.
            //Debug.ReadKey();
        }

    }
}
