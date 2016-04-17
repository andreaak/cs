using System.Diagnostics;

namespace Patterns._03_Behavioral.Mediator._003_Shop.Colleagues
{
    class Farmer : Colleague
    {
        public Farmer(Mediator mediator)
            : base(mediator)
        {
        }

        public void GrowTomato()
        {
            string tomato = "Tomato";
            Debug.WriteLine(this.GetType().Name 
                + " raised " + tomato);
            mediator.Send(tomato, this);
        }
    }
}
