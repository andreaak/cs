namespace Patterns._01_Creational.AbstractFactory._001_Game.Door
{
    // Класс двери для которой требуется заклинание.
    class DoorNeedingSpell : Door
    {
        // Конструктор.       
        public DoorNeedingSpell(Room.Room room1, Room.Room room2)
            : base(room1, room2)
        {
        }
    }
}
