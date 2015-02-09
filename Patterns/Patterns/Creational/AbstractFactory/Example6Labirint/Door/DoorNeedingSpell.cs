using System;


namespace Patterns.Creational.AbstractFactory.Example6Labirint
{
    // Класс двери для которой требуется заклинание.
    class DoorNeedingSpell : Door
    {
        // Конструктор.       
        public DoorNeedingSpell(Room room1, Room room2)
            : base(room1, room2)
        {
        }
    }
}
