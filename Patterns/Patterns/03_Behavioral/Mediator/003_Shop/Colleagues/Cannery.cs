using System.Diagnostics;

namespace Patterns._03_Behavioral.Mediator._003_Shop.Colleagues
{
    // Консервный завод.
    class Cannery : Colleague
    {
        public Cannery(Mediator mediator)
            : base(mediator)
        {
        }

        public void MakeKetchup(string message)
        {
            string ketchup = message + "Ketchup";
            Debug.WriteLine(this.GetType().Name 
                + " produced " + ketchup);
            mediator.Send(ketchup, this);
        }
    }
}
