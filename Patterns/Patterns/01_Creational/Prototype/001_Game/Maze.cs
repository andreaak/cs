using System.Collections.Generic;

namespace Patterns._01_Creational.Prototype._001_Game
{
    class Maze
    {
        // Поля.
        Dictionary<int, Room.Room> rooms = null;

        // Конструктор.
        public Maze()
        {
            // Создание массива комнат в лабиринте.
            this.rooms = new Dictionary<int, Room.Room>();
        }
        // Копирующий конструктор.
        protected Maze(Maze other)
        {
            this.rooms = other.rooms;
        }

        // Методы.

        // Добавление комнат в лабиринт.
        public void AddRoom(Room.Room room)
        {
            rooms.Add(room.RoomNumber, room);
        }

        // Возвращает ссылку на комнату.
        public Room.Room RoomNo(int number)
        {
            return rooms[number];
        }

        // Клонирование.
        public Maze Clone()
        {
            return new Maze(this);
        }
    }
}
