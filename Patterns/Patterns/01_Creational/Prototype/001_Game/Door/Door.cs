﻿using System.Diagnostics;

namespace Patterns._01_Creational.Prototype._001_Game.Door
{
    class Door : MapSite
    {
        // Поля.
        Room.Room room1 = null;
        Room.Room room2 = null;
        bool isOpen;

        // Конструкторы.

        public Door()
        {
        }

        public Door(Door other)
        {
            this.room1 = other.room1;
            this.room2 = other.room2;
        }

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

        // Клонирование.
        public virtual Door Clone()
        {
            Door door = new Door(this.room1, this.room2);
            // При обращении к закрытым членам экземпляра, в пределах того-же класса - инкапсуляция не работает.
            door.isOpen = this.isOpen;

            return door;
        }

        // Метод инициализации...
        public virtual void Initialize(Room.Room room1, Room.Room room2)
        {
            this.room1 = room1;
            this.room2 = room2;
        }
    }

}
