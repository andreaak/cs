﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        // Объект не может скрывать (инкапсулировать) ничего, от другого объекта того же класса.
        public void CallMethod(MyClass my)
        {
            // private метод виден на экземпляре!
            my.PrivateMethod();
        }

        private void PrivateMethod()
        {
            Debug.WriteLine(field);
        }


    }
    
    
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestClassBase1()
        {
            // 1. Создаем экземпляр класса MyClass (по сильной ссылке).
            // 2. Создаем экземпляр класса MyClass с именем instance.
            // 3. Инстанцируем класс MyClass.
            // 4. Создаем переменную с именем instance, типа MyClass и присваиваем ей адрес экземпляра на куче. 
            // (instance - является ссылкой на экземпляр класса MyClass построенный на куче)

            MyClass instance = new MyClass();
            /*
            Оператор new выполняет следующие действия:
            1.  Вычисление количества байтов, необходимых для хранения всех экземпляр-
            ных полей типа и всех его базовых типов, включая System.Object (в котором 
            отсутствуют собственные экземплярные поля). Кроме того, в каждом объекте 
            кучи должны присутствовать дополнительные члены, называемые указателем 
            на объект-тип (type object pointer) и индексом блока синхронизации (sync block 
            index); они необходимы CLR для управления объектом. Байты этих дополни-
            тельных членов добавляются к байтам, необходимым для размещения самого 
            объекта.
            2.  Выделение памяти для объекта с резервированием необходимого для данного 
            типа количества байтов в управляемой куче. Выделенные байты инициализи-
            руются нулями (0).
            3.  Инициализация указателя на объект-тип и индекса блока синхронизации.
            4.  Вызов конструктора экземпляра типа с параметрами, указанными при вызове 
            new. Большинство компиляторов автоматически включает в конструктор код вызова конструктора 
            базового класса. Каждый конструктор выполняет инициализацию определенных 
            в соответствующем типе полей. В частности, вызывается конструктор System.
            Object, но он ничего не делает и просто возвращает управление.
            */

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