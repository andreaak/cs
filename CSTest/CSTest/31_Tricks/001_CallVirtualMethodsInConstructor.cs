using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using NUnit.Framework;

namespace CSTest._31_Tricks
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
    public class _001_CallVirtualMethodsInConstructor
    {
        [Test]
        // Создать и запустить задачу на исполнение. 
        public void TestCallVirtualMethodsInConstructor1()
        {
            var p1 = ProductFactory.Create<ConcreteProduct>();
            /*
            ConcreteProduct: construction
            ConcreteProduct: post construction
            */
        }
    }

    public abstract class Product
    {
        protected internal abstract void PostConstruction();
    }

    public class ConcreteProduct : Product
    {
        // Внутренний конструктор не позволит клиентам иерархии
        // создавать объекты напрямую.
        public ConcreteProduct()
        {
            Debug.WriteLine("ConcreteProduct: construction");
        }

        protected internal override void PostConstruction()
        {
            Debug.WriteLine("ConcreteProduct: post construction");
        }
    }

    // Единственно законный способ создания объектов семейства Product
    public static class ProductFactory
    {
        public static T Create<T>() where T : Product, new()
        {
            try
            {
                var t = new T();
                // Вызываем постобработку
                t.PostConstruction();
                return t;
            }
            catch (TargetInvocationException e)
            {
                // «разворачиваем» исключение и бросаем исходное
                var edi = ExceptionDispatchInfo.Capture(e.InnerException);
                edi.Throw();
                // эта точка недостижима, но компилятор об этом не знает!
                return default(T);
            }
        }
    }

    /*
    Обратите внимание на реализацию метода Create и перехват TargetInvocationException. 
    Поскольку конструкция вида new T() использует отражение для
    создания экземпляра типа T, то в случае возникновения исключения 
    в конструкторе типа T исходное исключение будет «завернуто» в TargetInvocationException. 
    Чтобы упростить работу с нашей фабрикой, можно «развернуть» это исключение в методе Create 
    и пробросить исходное исключение с сохранением стека вызовов с помощью ExceptionDispatchInfo1 
    */
}
