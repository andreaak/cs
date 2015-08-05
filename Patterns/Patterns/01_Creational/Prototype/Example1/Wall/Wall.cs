using System;


namespace Creational.Prototype.Example1
{
    class Wall : MapSite
    {
        // Конструктор.
        public Wall()
        {
        }

        public override void Enter()
        {
            Console.WriteLine("Wall");
        }

        // Клонирование (Прототип)
        public virtual Wall Clone()
        {
            return new Wall();
        }
    }
}
