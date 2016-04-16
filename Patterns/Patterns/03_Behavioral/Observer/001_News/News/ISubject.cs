namespace Patterns._03_Behavioral.Observer._001_News.News
{
    public interface ISubject
    {
        void RegisterObserver(Widgets.IObserver observer);
        void RemoveObserver(Widgets.IObserver observer);
        void NotifyObservers();
    }
}
