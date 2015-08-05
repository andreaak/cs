using System;
using System.Diagnostics;


namespace Creational.FactoryMethod.Example1
{
    class Door : MapSite
    {
        // Поля.
        Room room1 = null;
        Room room2 = null;

        //Конструктор.       
        public Door(Room room1, Room room2)
        {
            this.room1 = room1;
            this.room2 = room2;
        }

        // Методы.

        // Отображает дверь.
        public override void Enter()
        {
            Console.WriteLine("Door");
        }

        // Метод возвращает ссылку на другую комнату.
        public Room OtherSideFrom(Room room)
        {
            if (room == room1)  // Если идем из r1 в r2, то возвращаем ссылку на r2.
                return room2;
            else          // Иначе: Если идем из r2 в r1, то возвращаем ссылку на r1.
                return room1;
        }
    }
}
