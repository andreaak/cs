using Patterns._01_Creational.Buider._001_Game.Door;
using Patterns._01_Creational.Buider._001_Game.Room;

namespace Patterns._01_Creational.Buider._001_Game.Factory
{
    // Класс  EnchantedMazeFactory создает компоненты волшебного лабиринта.
    class EnchantedMazeFactory : MazeFactory
    {
        // Конструктор.
        public EnchantedMazeFactory()
        {
        }

        public override Room.Room MakeRoom(int number)
        {
            return new EnchantedRoom(number, CastSpell());
        }

        public override Door.Door MakeDoor(Room.Room room1, Room.Room room2)
        {
            return new DoorNeedingSpell(room1, room2);
        }

        protected Spell CastSpell()
        {
            return null;
        }
    }
}
