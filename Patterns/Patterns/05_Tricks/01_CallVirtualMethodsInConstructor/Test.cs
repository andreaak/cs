using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._05_Tricks._01_CallVirtualMethodsInConstructor
{
    /*
    Существует проблема, с которой сталкиваются практически все разработчики
    независимо от используемого языка программирования: как гарантировать вызов
    виртуального метода при конструировании любого объекта определенной иерархии
    типов? 
    Вызов виртуального метода в конструкторе базового класса не подходит:
    в языке C# это может привести к непредсказуемому поведению, так как будет
    вызван метод наследника, конструктор которого еще не отработал. Можно восполь-
    зоваться приемами аспектно-ориентированного программирования, а можно вос-
    пользоваться фабрикой, которая вызовет виртуальный метод уже после создания
    экземпляра. 
    */

    [TestFixture]
    public class Test
    {
        [Test]
        // Создать и запустить задачу на исполнение. 
        public void TestCallVirtualMethodsInConstructor1()
        {
            var p1 = ProductFactory.Create<ConcreteProduct>();
            Debug.WriteLine("");
            var p2 = ProductFactory2.Create<ConcreteProduct>(5);
            /*
            Product: ctor
            ConcreteProduct: construction
            ConcreteProduct: post construction

            Product: ctor
            ConcreteProduct: construction with param: 5
            ConcreteProduct: post construction
            */
        }

        [Test]
        // Создать и запустить задачу на исполнение. 
        public void TestCallVirtualMethodsInConstructor3Interface()
        {
            var p1 = PostConstructionFactory.Create<ConcreteProduct2>();
            Debug.WriteLine("");
            var p2 = PostConstructionFactory.Create<ConcreteProduct2>(5);
            Debug.WriteLine("");
            var p3 = PostConstructionFactory.Create(() => new ConcreteProduct2(9));
            /*
            ConcreteProduct2: construction
            ConcreteProduct2: post construction

            ConcreteProduct2: construction with param: 5
            ConcreteProduct2: post construction

            ConcreteProduct2: construction with param: 9
            ConcreteProduct2: post construction
            */
        }
    }
}
