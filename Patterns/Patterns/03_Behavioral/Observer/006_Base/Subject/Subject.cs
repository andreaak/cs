using Patterns._03_Behavioral.Observer._006_Base.Observer;

namespace Patterns._03_Behavioral.Observer._006_Base.Subject
{
    abstract class Subject
    {
        public ConcreteObserver aConcreteObserver {set; get;}
        public ConcreteObserver anotherConcreteObserver { set; get; }

        public abstract void SetState(string state);
        public abstract string GetState();
        
        public void Notify()
        {
            aConcreteObserver.Update();
            anotherConcreteObserver.Update();
        }
    }
}
