using System;
using System.Diagnostics;

namespace Patterns._03_Behavioral.Observer._004_IObserver.Observer
{
    class ConcreteObserver : IObserver<string>
    {
        string name;
        string observerState;
        IDisposable unsubscriber;

        public ConcreteObserver(string name, IObservable<string> subject)
        {
            this.name = name;
            unsubscriber = subject.Subscribe(this);
        }

        // Реализация интерфейса IObserver<T>

        public void OnCompleted()
        {
            unsubscriber.Dispose();
        }

        public void OnError(Exception error)
        {
            //Debug.ForegroundColor = ConsoleColor.Red;
            Debug.WriteLine("Observer {0}, Error: {1}", name, error.Message);
            //Debug.ForegroundColor = ConsoleColor.Gray;
        }

        // Аналог Update(argument) - модель проталкивания.
        public void OnNext(string value)
        {
            observerState = value;
            Console.WriteLine("Observer {0}, State = {1}", name, observerState);
        }
    }
}
