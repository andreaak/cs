using System.Diagnostics;

namespace Patterns._01_Creational.Singleton._001_Game.Wall
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

        // Клонироание (Прототип)
        public virtual Wall Clone()
        {
            return new Wall();
        }
    }
}
