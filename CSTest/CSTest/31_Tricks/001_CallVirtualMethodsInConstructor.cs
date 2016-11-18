using System;
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
            var p2 = ProductFactory2.Create<ConcreteProduct>(5);
            /*
            Product: ctor
            ConcreteProduct: construction
            ConcreteProduct: post construction

            Product: ctor
            ConcreteProduct: construction wit param
            ConcreteProduct: post construction
            */
        }

        [Test]
        // Создать и запустить задачу на исполнение. 
        public void TestCallVirtualMethodsInConstructor3Interface()
        {
            var p1 = PostConstructionFactory.Create<ConcreteProduct2>();
            var p2 = PostConstructionFactory.Create<ConcreteProduct2>(5);
            /*
            ConcreteProduct2: construction
            ConcreteProduct2: post construction
            ConcreteProduct2: construction wit param
            ConcreteProduct2: post construction
            */
        }
    }

    public abstract class Product
    {
        protected Product()
        {
            Debug.WriteLine("Product: ctor");
        }

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

        public ConcreteProduct(int w)
        {
            Debug.WriteLine("ConcreteProduct: construction wit param");
        }

        protected internal override void PostConstruction()
        {
            Debug.WriteLine("ConcreteProduct: post construction");
        }
    }
    public class ConcreteProduct2 : IPostConstruction
    {
        // Внутренний конструктор не позволит клиентам иерархии
        // создавать объекты напрямую.
        public ConcreteProduct2()
        {
            Debug.WriteLine("ConcreteProduct2: construction");
        }

        public ConcreteProduct2(int w)
        {
            Debug.WriteLine("ConcreteProduct2: construction wit param");
        }

        public void PostConstruction()
        {
            Debug.WriteLine("ConcreteProduct2: post construction");
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
#if CS5
                var edi = ExceptionDispatchInfo.Capture(e.InnerException);
                edi.Throw();
#endif
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

    public static class ProductFactory2
    {
        public static T Create<T>(params object[] parameters) where T : Product, new()
        {
            try
            {
                T t = (T)Activator.CreateInstance(typeof(T), parameters);
                // Вызываем постобработку
                t.PostConstruction();
                return t;
            }
            catch (TargetInvocationException e)
            {
                // «разворачиваем» исключение и бросаем исходное
#if CS5
                var edi = ExceptionDispatchInfo.Capture(e.InnerException);
                edi.Throw();
#endif
                // эта точка недостижима, но компилятор об этом не знает!
                return default(T);
            }
        }
    }

    public interface IPostConstruction
    {
        void PostConstruction();
    }

    public static class PostConstructionFactory
    {
        public static T Create<T>(params object[] parameters) where T : IPostConstruction
        {
            T t = (T)Activator.CreateInstance(typeof(T), parameters);
            // Вызываем постобработку
            t.PostConstruction();
            return t;
        }
    }
}
