using System;
using System.Diagnostics;


namespace Creational.FactoryMethod.Example1
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
