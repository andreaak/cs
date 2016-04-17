using System.Diagnostics;

namespace Patterns._03_Behavioral.Mediator._003_Shop.Colleagues
{
    class Shop : Colleague
    {
         public Shop(Mediator mediator)
            : base(mediator)
        {
        }
               
        public void SellKetchup(string message)
        {
            Debug.WriteLine(this.GetType().Name 
                + " sold " + message);
        }
    }
}
