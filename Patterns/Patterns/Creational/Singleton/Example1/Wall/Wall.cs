using System;


namespace Patterns.Creational.Singleton.Example1
{
    class Wall : MapSite
    {
        // Конструктор.
        public Wall()
        {
        }

        public override void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Wall");
        }

        // Клонироание (Прототип)
        public virtual Wall Clone()
        {
            return new Wall();
        }
    }
}
