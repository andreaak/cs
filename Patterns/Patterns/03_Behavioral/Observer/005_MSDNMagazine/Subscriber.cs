using System.Diagnostics;

namespace Patterns._03_Behavioral.Observer._005_MSDNMagazine
{
    abstract class AbstractSubscriber
    {
        public abstract void Deliver();
    }

    class Subscriber : AbstractSubscriber
    {
        private string name;
        private Magazine magazine;
        private MicrosoftPress publisher;

        public Subscriber(MicrosoftPress publisher, string name)
        {
            this.publisher = publisher;
            this.name = name;
        }

        public override void Deliver()
        {
            magazine = publisher.Magazine;
            Debug.WriteLine("��������� {0} ������� ������: {1} �� �������: \n {2}", 
                name, magazine.Title, magazine.Content);
        }
    }
}
