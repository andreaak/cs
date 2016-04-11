using System.Diagnostics;

namespace Patterns.Creational.Singleton._001_Game
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
