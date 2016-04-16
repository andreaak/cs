using System.Diagnostics;

namespace Patterns._01_Creational.Prototype._001_Game.Wall
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

        // Клонирование (Прототип)
        public virtual Wall Clone()
        {
            return new Wall();
        }
    }
}
