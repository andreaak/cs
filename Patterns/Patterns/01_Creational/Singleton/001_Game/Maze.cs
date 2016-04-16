using System.Collections.Generic;

namespace Patterns._01_Creational.Singleton._001_Game
{
    class Maze
    {
        Dictionary<int, Room.Room> rooms = null;

        // Конструктор.
        public Maze()
        {
            // Создание массива комнат в лабиринте.
            this.rooms = new Dictionary<int, Room.Room>();
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
            Maze maze = new Maze();
            maze.rooms = this.rooms;

            return maze;
        }
    }
}
