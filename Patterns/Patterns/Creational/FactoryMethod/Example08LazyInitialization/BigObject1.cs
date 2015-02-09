using System;
using System.Diagnostics;
using System.Threading;

/*
    Отложенная (ленивая) инициализация (англ. Lazy initialization). 
    Приём в программировании, когда некоторая ресурсоёмкая операция (создание объекта, вычисление значения) 
    выполняется непосредственно перед тем, как будет использован её результат. 
    Таким образом, инициализация выполняется «по требованию», а не заблаговременно. 
    Аналогичная идея находит применение в самых разных областях: 
    например, компиляция «на лету» и логистическая концепция «Точно в срок». 
    
    Частный случай ленивой инициализации — создание объекта в момент обращения к нему — 
    является одним из порождающих шаблонов проектирования. 
    Как правило, он используется в сочетании с такими шаблонами как Фабричный метод, Одиночка и Заместитель.
*/

namespace Creational.FactoryMethod.Example8
{
    public class LazyInitialization1<T> where T : new()
    {
        protected LazyInitialization1() { }

        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (typeof(T))
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                }
                return instance;
            }
        }
    }

    public sealed class BigObject1 : LazyInitialization1<BigObject1>
    {
        public BigObject1()
        {
            // Большая работа.
            Thread.Sleep(3000);
            Debug.WriteLine("Экземпляр BigObject создан");
        }
    }
}
