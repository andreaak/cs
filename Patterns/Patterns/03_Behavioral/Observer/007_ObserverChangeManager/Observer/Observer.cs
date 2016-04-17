using System.Diagnostics;

namespace Patterns._03_Behavioral.Observer._007_ObserverChangeManager.Observer
{
    class Observer
    {
        public string Name { get; private set; }

        public Observer(string name)
        {
            Name = name;
        }

        public void Update(Subject.Subject subject)
        {
            Debug.WriteLine("{0} -> {1}", subject.Name, this.Name);
        }
    }
}
