using System.Diagnostics;

namespace Patterns._01_Creational.FactoryMethod._001_Game.Door
{
    class Door : MapSite
    {
        // Поля.
        Room.Room room1 = null;
        Room.Room room2 = null;

        //Конструктор.       
        public Door(Room.Room room1, Room.Room room2)
        {
            this.room1 = room1;
            this.room2 = room2;
        }

        // Методы.

        // Отображает дверь.
        public override void Enter()
        {
            Debug.WriteLine("Door");
        }

        // Метод возвращает ссылку на другую комнату.
        public Room.Room OtherSideFrom(Room.Room room)
        {
            if (room == room1)  // Если идем из r1 в r2, то возвращаем ссылку на r2.
                return room2;
            else          // Иначе: Если идем из r2 в r1, то возвращаем ссылку на r1.
                return room1;
        }
    }
}
