using NUnit.Framework;
using System.Diagnostics;
using CSTest._04_Class._00_Base._0_Setup;

namespace CSTest._04_Class._00_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestClass1Creation()
        {
            /*
             1. Создаем экземпляр класса MyClass (по сильной ссылке).
             2. Создаем экземпляр класса MyClass с именем instance.
             3. Инстанцируем класс MyClass.
             4. Создаем переменную с именем instance, типа MyClass и присваиваем ей адрес экземпляра на куче. 
             (instance - является ссылкой на экземпляр класса MyClass построенный на куче)
            */

            TestClass instance = new TestClass();
            /*
            Оператор new выполняет следующие действия:
            1.  Вычисление количества байтов, необходимых для хранения всех экземплярных полей типа 
            и всех его базовых типов, включая System.Object (в котором 
            отсутствуют собственные экземплярные поля). Кроме того, в каждом объекте 
            кучи должны присутствовать дополнительные члены, называемые указателем 
            на объект-тип (type object pointer) и индексом блока синхронизации (sync block index); 
            они необходимы CLR для управления объектом. 
            Байты этих дополнительных членов добавляются к байтам, необходимым для размещения самого объекта.
            2.  Выделение памяти для объекта с резервированием необходимого для данного 
            типа количества байтов в управляемой куче. Выделенные байты инициализируются нулями (0).
            3.  Инициализация указателя на объект-тип и индекса блока синхронизации.
            4.  Вызов конструктора экземпляра типа с параметрами, указанными при вызове 
            new. Большинство компиляторов автоматически включает в конструктор код вызова конструктора 
            базового класса. Каждый конструктор выполняет инициализацию определенных 
            в соответствующем типе полей. В частности, вызывается конструктор System.
            Object, но он ничего не делает и просто возвращает управление.
            */

            // Создание экземпляра по слабой ссылке. (Анонимные объекты)
            new TestClass().InstanceMethod();

            /*
            Null
            */
        }

        [Test]
        public void TestClass2InstanceFieldsAndMethods()
        {
            TestClass instance = new TestClass();

            // Полю field, экземпляра instance, присваиваем значение Hello world!
            instance.instanceField = "Hello world!";
            Debug.WriteLine(instance.instanceField);

            // Вызов метода с именем InstanceMethod на экземпляре instance.
            instance.InstanceMethod();
            /*
            Hello world!
            Hello world!
            */
        }

        [Test]
        public void TestClass2StaticFieldsAndMethods()
        {
            TestClass.staticField = "Hello world! Static";

            Debug.WriteLine(TestClass.staticField);

            // Вызов метода с именем StaticMethod.
            TestClass.StaticMethod();
            /*
            Hello world! Static
            Hello world! Static
            */
        }
    }
}
