using System;
using System.Collections.Generic;


namespace Creational.Prototype.Example1
{
    class Maze
    {
        // Поля.
        Dictionary<int, Room> rooms = null;

        // Конструктор.
        public Maze()
        {
            // Создание массива комнат в лабиринте.
            this.rooms = new Dictionary<int, Room>();
        }
        // Копирующий конструктор.
        protected Maze(Maze other)
        {
            this.rooms = other.rooms;
        }

        // Методы.

        // Добавление комнат в лабиринт.
        public void AddRoom(Room room)
        {
            rooms.Add(room.RoomNumber, room);
        }

        // Возвращает ссылку на комнату.
        public Room RoomNo(int number)
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
