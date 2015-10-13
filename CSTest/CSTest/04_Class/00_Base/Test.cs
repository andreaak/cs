using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._04_Class._00_Base
{
    // Создаем класс с именем MyClass.
    // В теле класса создаем открытое поле типа string с именем field и метод с именем Method.

    class MyClass
    {
        public string field;

        public void Method()
        {
            Debug.WriteLine(field);
        }
    }
    
    
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            // 1. Создаем экземпляр класса MyClass (по сильной ссылке).
            // 2. Создаем экземпляр класса MyClass с именем instance.
            // 3. Инстанцируем класс MyClass.
            // 4. Создаем переменную с именем instance, типа MyClass и присваиваем ей адрес экземпляра на куче. 
            // (instance - является ссылкой на экземпляр класса MyClass построенный на куче)

            MyClass instance = new MyClass();

            // Полю field, экземпляра instance, присваиваем значение Hello world!

            instance.field = "Hello world!";

            // Выводим на экран значение поля field экземпляра instance.

            Debug.WriteLine(instance.field);

            // Вызов метода с именем Method на экземпляре instance.
            instance.Method();

            // Создание экземпляра по слабой ссылке. (Анонимные объекты)
            new MyClass().Method();

        }
    }
}
