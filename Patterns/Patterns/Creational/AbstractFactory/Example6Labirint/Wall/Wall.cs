using System;
using System.Diagnostics;


namespace Patterns.Creational.AbstractFactory.Example6Labirint
{
    class Wall : MapSite
    {
        // Конструктор.
        public Wall()
        {
        }

        public override void Enter()
        {
            Debug.WriteLine("Wall");
        }
    }
}
