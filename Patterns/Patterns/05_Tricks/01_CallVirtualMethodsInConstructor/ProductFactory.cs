using System;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Patterns._05_Tricks._01_CallVirtualMethodsInConstructor
{
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

    ////////////////////////////////////////////
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

    ////////////////////////////////////////////
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

        public static T Create<T>(Func<T> _factoryMethod) where T : IPostConstruction
        {
            T t = _factoryMethod();
            // Вызываем постобработку
            t.PostConstruction();
            return t;
        }
    }

}
