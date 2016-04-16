using System.Diagnostics;

namespace Patterns._01_Creational.AbstractFactory._001_Game.Wall
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
