namespace Patterns._03_Behavioral.Observer._001_News.Widgets
{
    public interface IObserver
    {
        void Update(string twitter, string lenta, string tv);
        void RemoveFromSubject();
    }
}
