using System;
using System.Diagnostics;


namespace Creational.FactoryMethod._001_Game
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
