using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._03_Behavioral.Observer._003_Observer_Event.Subject;

// � ���������� ������ .Net ���������� �������� �������.

namespace Patterns._03_Behavioral.Observer._003_Observer_Event
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            // ��������.
            Subject.Subject subject = new ConcreteSubject();

            // ���������, � ���������� ������ ����������.
            Observer.Observer observer = new Observer.Observer((observerState) => Console.WriteLine(observerState + " 1"));

            // �������� �� ����������� � �������.
            subject.Event += observer;
            subject.Event += (observerState) => Console.WriteLine(observerState + " 2");

            subject.State = "State ...";
            subject.Notify();

            Debug.WriteLine(new string('-', 11));

            // ������� �� �����������.
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

            // ������ ������ Notify() - ��������� ��������� � �������� (Subject).
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
