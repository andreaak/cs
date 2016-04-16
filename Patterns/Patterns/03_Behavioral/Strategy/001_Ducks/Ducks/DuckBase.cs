using System.Diagnostics;
using Patterns._03_Behavioral.Strategy._001_Ducks.Fly;
using Patterns._03_Behavioral.Strategy._001_Ducks.Quack;

namespace Patterns._03_Behavioral.Strategy._001_Ducks.Ducks
{
    public abstract class DuckBase
    {
        protected IFlyable flyBehaviour;
        protected IQuackable quackBehaviour;

        public DuckBase()
        {
            flyBehaviour = new FlyWithWings();
            quackBehaviour = new SimpleQuack();
        }

        public void SetQuackBehaviour(IQuackable newQuackBehaviour)
        {
            quackBehaviour = newQuackBehaviour;
        }

        public void SetFlyBehaviour(IFlyable newFlyBehaviour)
        {
            flyBehaviour = newFlyBehaviour;
        }
        
        public void Swim()
        {
            Debug.WriteLine("I'm swimming");
        }

        public void Quack()
        {
            quackBehaviour.Quack();
        }

        public void Fly()
        {
            flyBehaviour.Fly();
        }

        public abstract void Display();
    }
}
