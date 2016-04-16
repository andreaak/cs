using Patterns._01_Creational.FactoryMethod._001_Game.Door;
using Patterns._01_Creational.FactoryMethod._001_Game.Room;

namespace Patterns._01_Creational.FactoryMethod._001_Game
{
    class EnchantedMazeGame : MazeGame.MazeGame
    {
        // Конструктор.
        public EnchantedMazeGame()
        {
        }

        // Методы.

        public override Room.Room MakeRoom(int number)
        {
            return new EnchantedRoom(number, this.CastSpell());
        }

        public override Door.Door MakeDoor(Room.Room r1, Room.Room r2)
        {
            return new DoorNeedingSpell(r1, r2);
        }

        // Метод создания заклинания.
        protected Spell CastSpell()
        {
            return new Spell();
        }
    }
}
