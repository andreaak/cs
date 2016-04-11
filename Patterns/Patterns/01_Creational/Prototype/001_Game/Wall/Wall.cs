using System.Diagnostics;

namespace Creational.Prototype._001_Game
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
