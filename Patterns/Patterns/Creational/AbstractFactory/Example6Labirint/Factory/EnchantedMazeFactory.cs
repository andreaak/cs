using System;


namespace Patterns.Creational.AbstractFactory.Example6Labirint
{
    // Класс  EnchantedMazeFactory создает компоненты волшебного лабиринта.
    class EnchantedMazeFactory : MazeFactory
    {
        // Конструктор.
        public EnchantedMazeFactory()
        {
        }

        public override Room MakeRoom(int number)
        {
            return new EnchantedRoom(number, CastSpell());
        }

        public override Door MakeDoor(Room room1, Room room2)
        {
            return new DoorNeedingSpell(room1, room2);
        }

        protected Spell CastSpell()
        {
            return null;
        }
    }
}
