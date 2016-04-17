using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._03_Behavioral.Observer._003_Observer_Event.Subject;

// В событийной модели .Net обзервером является делегат.

namespace Patterns._03_Behavioral.Observer._003_Observer_Event
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            // Издатель.
            Subject.Subject subject = new ConcreteSubject();

            // Подписчик, с сообщенным лямбда выражением.
            Observer.Observer observer = new Observer.Observer((observerState) => Console.WriteLine(observerState + " 1"));

            // Подписка на уведомление о событии.
            subject.Event += observer;
            subject.Event += (observerState) => Console.WriteLine(observerState + " 2");

            subject.State = "State ...";
            subject.Notify();

            Debug.WriteLine(new string('-', 11));

            // Отписка от уведомлений.
            subject.Event -= observer;
            subject.Notify();

            // Delay.
            //Console.ReadKey();
        }

        delegate void SubjectObserver();
        [TestMethod]
        public void Test2()
        {
            SubjectObserver so = new SubjectObserver(Update);

            // Аналог вызова Notify() - логически относится к издателю (Subject).
            so.Invoke();

            // Delay.
            //Console.ReadKey();
        } 

        static void Update()
        {
            Debug.WriteLine("Hello world!");
        }
    }
}
