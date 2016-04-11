using System;


namespace Creational.FactoryMethod._001_Game
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
