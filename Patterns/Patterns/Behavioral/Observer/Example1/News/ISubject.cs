using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Observer.Example1.News
{
    public interface ISubject
    {
        void RegisterObserver(Widgets.IObserver observer);
        void RemoveObserver(Widgets.IObserver observer);
        void NotifyObservers();
    }
}
